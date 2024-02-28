using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a function call - like (myfunc 1 2 3)
    /// </summary>
    public class CallExpression : IExpression
    {
        private readonly IExpression _expression;
        private readonly IEnumerable<IExpression> _arguments;

        public CallExpression(IExpression expression, params IExpression[] arguments)
        {
            _expression = expression;
            _arguments = arguments;
        }

        /// <summary>
        /// Firstly, we evaluate the Function to a Closure.
        /// Then we populate the Closure's environment with function arguments.
        /// Finally we map the Closure to itself, so that recursion will remember the inner environment.
        /// The Function's body can then be evaluated to a Value.
        /// </summary>
        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            var closure = _expression.Evaluate(environment) as Closure;

            if (closure == null)
            {
                throw new SyntaxErrorException("Closure for the function is not found...");
            }

            var environmentUpdated = new Dictionary<string, IExpression>(closure.Environment);
            var originalParameters = closure.Function.Parameters.Zip(_arguments, (x, y) => Tuple.Create(x, y));
            
            foreach (var param in originalParameters)
            {
                environmentUpdated[param.Item1] = param.Item2.Evaluate(environment);
            }

            environmentUpdated[closure.Function.Name] = closure;

            return closure.Function.Body.Evaluate(environmentUpdated);
        }
    }
}

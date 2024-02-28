using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a conditional expression - like (if (e1) e2 e3)
    /// </summary>
    public class ConditionalExpression : IExpression
    {
        private readonly IExpression _booleanExpression;
        private readonly IExpression _trueBranch;
        private readonly IExpression _elseBranch;

        public ConditionalExpression(IExpression booleanExpression, IExpression trueBranch, IExpression elseBranch)
        {
            _booleanExpression = booleanExpression;
            _trueBranch = trueBranch;
            _elseBranch = elseBranch;
        }
        
        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            var condition = _booleanExpression.Evaluate(environment) as BoolValue;

            if (condition == null)
            {
                throw new SyntaxErrorException("Condition is not boolean...");
            }

            return condition.Value ? _trueBranch.Evaluate(environment)
                : _elseBranch.Evaluate(environment);
        }
    }
}

using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a variable reference
    /// </summary>
    public class Variable : IExpression
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }

        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            if (environment[Name] is not Value)
            {
                throw new SyntaxErrorException("Incorrect variable value...");
            }

            return (Value)environment[Name];
        }
    }
}

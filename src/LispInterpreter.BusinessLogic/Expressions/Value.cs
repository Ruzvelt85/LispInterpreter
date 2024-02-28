namespace LispInterpreter.BusinessLogic.Expressions
{
    public abstract class Value : IExpression
    {
        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            return this;
        }
    }
}

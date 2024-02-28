namespace LispInterpreter.BusinessLogic.Expressions
{
    public interface IExpression
    {
        Value Evaluate(IDictionary<string, IExpression> environment);
    }
}

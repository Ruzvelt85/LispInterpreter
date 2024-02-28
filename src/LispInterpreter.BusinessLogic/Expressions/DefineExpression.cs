namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Auxiliary empty class which we need for  correct output of define expressions
    /// </summary>
    public class DefineExpression : IExpression
    {
        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            throw new NotImplementedException();
        }
    }
}

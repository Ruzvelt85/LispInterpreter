namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a function with a range of parameters
    /// </summary>
    public class Function : IExpression
    {
        public string Name { get; }
        public IExpression Body { get; }
        public IEnumerable<string> Parameters { get; }

        public Function(string name, IExpression body, params string[] parameters)
        {
            Name = name; 
            Body = body; 
            Parameters = parameters;
        }

        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            return new Closure(this, environment);
        }
    }
}

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a function closure (what functions evaluates to)
    /// Closures make local environments (lexical scoping) possible
    /// </summary>
    public class Closure : Value
    {
        public Function Function { get; }
        public IDictionary<string, IExpression> Environment { get; }

        public Closure(Function function, IDictionary<string, IExpression> environment)
        {
            Function = function;
            Environment = environment;
        }
    }
}

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents an integer constant
    /// </summary>
    public class NumberValue : Value
    {
        public int Value { get; }

        public NumberValue(int value)
        {
            Value = value;
        }
    }
}

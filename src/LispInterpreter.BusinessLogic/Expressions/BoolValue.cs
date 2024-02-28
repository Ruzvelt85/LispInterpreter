namespace LispInterpreter.BusinessLogic.Expressions
{
    public class BoolValue : Value
    {
        public bool Value { get; }

        public BoolValue(bool value)
        {
            Value = value;
        }
    }
}

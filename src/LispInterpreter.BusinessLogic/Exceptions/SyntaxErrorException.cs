namespace LispInterpreter.BusinessLogic.Exceptions
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException(string message)
            : this(message, null)
        {
        }

        public SyntaxErrorException(string message, Exception? inner)
            : base(message, inner)
        {
        }
    }
}

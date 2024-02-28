namespace LispInterpreter.BusinessLogic
{
    public static class Tokenizer
    {
        /// <summary>
        /// Splits the expression into list of tokens
        /// </summary>
        public static List<string> Tokenize(string expression)
        {
            return expression
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}

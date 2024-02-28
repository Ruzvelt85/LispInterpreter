using LispInterpreter.BusinessLogic;
using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.ConsoleApplication;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===A simple LISP Interpreter===");

        var interpreter = new Interpreter();

        while (true)
        {
            Console.Write("Enter input (or type 'exit' to quit): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                string result = interpreter.Process(input);
                Console.WriteLine("Output: " + result);
            }
            catch (SyntaxErrorException ex)
            {
                Console.WriteLine("An invalid input: an expression cannot be interpreted as a LISP expression. Details: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred. Details: " + ex.Message);
            }
        }

        Console.WriteLine("Exiting the program...");
    }
}
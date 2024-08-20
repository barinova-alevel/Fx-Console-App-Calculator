using System.Text.RegularExpressions;
using Calculator.Exceptions;
using Serilog;

namespace Calculator.UI
{
    public class InputOutput : IInputOutput
    {
        public string GetExpression(string stringToBeCheckedIfValidExpression)
        {
            try
            {
                if (stringToBeCheckedIfValidExpression != null)
                {
                    if (IsValidMathExpression(stringToBeCheckedIfValidExpression))
                    {
                        return stringToBeCheckedIfValidExpression;
                    }
                    else { throw new WrongInputException("The input is NOT a valid mathematical expression."); }
                }
                return "temp: 123";
            }
            catch (WrongInputException ex)
            {
                Log.Information(ex.Message);
                return "temp: wrong inp";
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Would you like to calculate an expression? (yes/no): ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "no")
                {
                    Environment.Exit(1);
                }

                else if (userInput != "yes")
                {
                    Log.Information("Invalid input. Please enter 'yes' or 'no'.");
                }

                else if (userInput == "yes")
                {
                    //add in mode 2 selection "write expression manually or from file"
                    Console.WriteLine("Please enter expression: ");
                    string userInp = ReadConsoleInput();
                    GetExpression(userInp);
                }
            }
        }

        private string ReadConsoleInput()
        {
            string userInput = Console.ReadLine().ToLower();
            return userInput;
        }

        //expression is not counting parentheses
        private bool IsValidMathExpression(string input)
        {
            string pattern = @"^\\s*[-+]?\\d+(\\.\\d+)?(\\s*[-+*/%^]\\s*[-+]?\\d+(\\.\\d+)?)*\\s*$ ";
            return Regex.IsMatch(input, pattern);
        }
    }
}

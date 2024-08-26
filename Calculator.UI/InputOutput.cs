using System.Text.RegularExpressions;
using Calculator.Exceptions;
using Calculator.BL;
using Serilog;

namespace Calculator.UI
{
    public class InputOutput : IInputOutput
    {
        public string GetExpression(string stringToBeCheckedIfValidExpression)
        {
            try
            {
                if (stringToBeCheckedIfValidExpression == null)
                {
                    Log.Information("String is null");
                    return "temp: wrong inp";
                }
                else
                {
                    if (IsValidMathExpression(stringToBeCheckedIfValidExpression))
                    {
                        return stringToBeCheckedIfValidExpression;
                    }
                    else
                    {
                        throw new WrongInputException("The input is NOT a valid mathematical expression.");
                    }
                }
            }
            catch (WrongInputException ex)
            {
                Log.Information(ex.Message);
                return "temp: wrong inp";
            }
        }

        public void Run()
        {
            CalculatorLogic calculator = new CalculatorLogic();

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
                    Console.WriteLine("Please enter expression: ");
                    try
                    {
                        string userExpression = ReadConsoleInput();
                        string validatedExpression = GetExpression(userExpression);
                        double result = calculator.EvaluateExpression(validatedExpression);
                        Log.Information($"{userExpression} result: {result}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Log.Information(ex.Message);
                        continue;
                    }
                    catch (DevideByZeroException)
                    {

                    }
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
            string pattern = @"^\s*[-+]?\d+(\.\d+)?(\s*[-+*/%^]\s*[-+]?\d+(\.\d+)?)*\s*$";
            return Regex.IsMatch(input, pattern);
        }
    }
}

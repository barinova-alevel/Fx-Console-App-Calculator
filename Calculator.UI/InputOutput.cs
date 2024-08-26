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
            if (IsValidMathExpression(stringToBeCheckedIfValidExpression))
            {
                return stringToBeCheckedIfValidExpression;
            }
            else
            {
                throw new WrongInputException("The input is NOT a valid mathematical expression.");
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
                    catch (InvalidOperatorException)
                    {
                        Log.Information("Invalid operator encountered.");
                        continue;
                    }
                    catch (DevideByZeroException)
                    {

                    }
                    catch (WrongInputException ex)
                    {
                        Log.Information(ex.Message);
                        continue;
                    }
                    catch (NullReferenceException ex)
                    {
                        Log.Debug(ex.Message);
                        continue;
                    }
                    catch (ArgumentNullException ex)
                    {
                        Log.Debug($"{ex.ParamName} can not be null \n{ex.StackTrace}");
                        continue;
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

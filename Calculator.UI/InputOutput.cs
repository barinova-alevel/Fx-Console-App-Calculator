﻿using Calculator.Exceptions;
using Calculator.BL;
using Serilog;
using Calculator.UI.Helper;

namespace Calculator.UI
{
    public class InputOutput : IInputOutput
    {
        public string GetExpression(string stringToBeCheckedIfValidExpression)
        {
            MathExpressionValidator validator = new MathExpressionValidator();
            if (validator.IsValidMathExpression(stringToBeCheckedIfValidExpression))
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
                    Console.WriteLine("Would you like to read expressions from the file? (yes/no):");
                    string userResponse = ReadConsoleInput();

                    try
                    {
                        if (userResponse == "yes")
                        {
                            string filePath = GetPathFromConsole();
                            FileAnalyzer fileAnalyzer = new FileAnalyzer(filePath);
                            LineIterator lineIterator = fileAnalyzer.GetIterator();
                            fileAnalyzer.Analyze(lineIterator);
                        }
                        else 
                        {
                            Console.WriteLine("Please enter expression: ");
                            string userExpression = ReadConsoleInput();
                            string validatedExpression = GetExpression(userExpression);
                            double result = calculator.EvaluateExpression(validatedExpression);
                            Log.Information($"{userExpression} result: {result}");
                        }
                    }
                    catch (DevideByZeroException ex)
                    {
                        Log.Information(ex.Message);
                        continue;
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
                    catch (Exception ex)
                    {
                        Log.Debug(ex.Message);
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

        private string GetPathFromConsole()
        {
            Log.Information("Enter file path manually:");

            string filePath = @"" + Console.ReadLine();
            Log.Debug($"Console file path: {filePath}");

            if (!IsValidPath(filePath))
            {
                if (TryAgainConsole("Invalid path"))
                {
                    return GetPathFromConsole();
                }

                Environment.Exit(1);
            }

            if (!File.Exists(filePath))
            {
                if (TryAgainConsole("File does not exist"))
                {
                    return GetPathFromConsole();
                }

                Environment.Exit(1);
            }

            return filePath;
        }

        private bool IsValidPath(string path)
        {
            bool isValid = false;
            try
            {
                isValid = Path.IsPathRooted(path) && !string.IsNullOrWhiteSpace(Path.GetFileName(path));
            }
            catch (Exception)
            {
                return false;
            }

            return isValid;
        }

        private bool TryAgainConsole(string failReason)
        {
            Log.Information($"{failReason}, would you like to try again? (yes/no)");
            string userInput = Console.ReadLine().ToLower();

            return userInput != "no";
        }
    }
}


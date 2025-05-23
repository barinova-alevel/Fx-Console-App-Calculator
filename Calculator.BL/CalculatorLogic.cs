﻿using Calculator.Exceptions;
namespace Calculator.BL
{
    public class CalculatorLogic
    {

        private static readonly HashSet<string> Operators = new HashSet<string> { "+", "-", "*", "/", "%" };
        public double EvaluateExpression(string expression)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();
            var priority = new PrioritiesOfOperations();

            if (IsConsecutiveOperators(expression))
            {
                throw new WrongInputException("Wrong input. Consecutive operators are not allowed.");
            }

            var tokens = Tokenize(expression);


            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    outputQueue.Enqueue(token);
                }
                else if (IsOperator(token))
                {
                    while (operatorStack.Count > 0 &&
                           IsOperator(operatorStack.Peek()) &&
                           priority.GetPrecedence(token) <= priority.GetPrecedence(operatorStack.Peek()))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            double result = EvaluatePostfix(outputQueue);
            return result;

        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var number = "";
            bool expectUnary = true;

            if (expression == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var symbol in expression)
            {
                if (char.IsDigit(symbol) || symbol == '.')
                {
                    number += symbol;
                    expectUnary = false;

                }

                else if (IsOperator(symbol.ToString()))
                {
                    if (number != "")
                    {
                        tokens.Add(number);
                        number = "";
                    }

                    if ((symbol == '+' || symbol == '-') && expectUnary)
                    {
                        number += symbol;
                    }
                    else
                    {
                        tokens.Add(symbol.ToString());
                        expectUnary = true;
                    }
                }
                else if (symbol == '(' || symbol == ')')
                {
                    if (number != "")
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    tokens.Add(symbol.ToString());
                    expectUnary = symbol == '(';
                }
            }

            if (number != "")
            {
                tokens.Add(number);
            }

            return tokens;
        }

        private bool IsOperator(string token)
        {
            return Operators.Contains(token);
        }

        private double EvaluatePostfix(Queue<string> outputQueue)
        {
            var stack = new Stack<double>();

            while (outputQueue.Count > 0)
            {
                var token = outputQueue.Dequeue();

                if (double.TryParse(token, out double number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(token))
                {
                    var right = stack.Pop();
                    var left = stack.Pop();

                    if (token == "/" && right == 0)
                    {
                        throw new DevideByZeroException();
                    }
                    else
                    {
                        stack.Push(ApplyOperator(token, left, right));
                    }
                }
            }

            if (stack.Count <= 0)
            {
                throw new WrongInputException();
            }
            else
            {
                return stack.Pop();
            }
        }

        private double ApplyOperator(string op, double left, double right)
        {
            switch (op)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
                case "%":
                    return left % right;
                default:
                    throw new ArgumentException("Invalid operator");
            }
        }

        private bool IsConsecutiveOperators(string expression)
        {
            string operators = "+-*/%";

            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (operators.Contains(expression[i]) && operators.Contains(expression[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

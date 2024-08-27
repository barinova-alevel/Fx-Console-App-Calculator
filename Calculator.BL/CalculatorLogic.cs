using Calculator.Exceptions;
namespace Calculator.BL
{
    public class CalculatorLogic
    {
        public double EvaluateExpression(string expression)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();
            var tokens = Tokenize(expression);
            var priority = new PrioritiesOfOperations();

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

            if (expression == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    number += c;
                }
                else if (IsOperator(c.ToString()))
                {
                    if (number != "")
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    tokens.Add(c.ToString());
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
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "%";
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
    }
}

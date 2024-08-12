using System;

class Program
{
    static void Main()
    {
        bool continueCalculation = true;

        while (continueCalculation)
        {
            Console.Clear();

            Console.WriteLine("Console Calculator\n");

            Console.Write("Enter the first number: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter an operator (+, -, *, /): ");
            char op = Console.ReadLine()[0];

            Console.Write("Enter the second number: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            double result = 0;
            bool validOperation = true;

            switch (op)
            {
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                        validOperation = false;
                    }
                    break;
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                default:
                    Console.WriteLine("Error: Invalid operator.");
                    validOperation = false;
                    break;
            }

            if (validOperation)
            {
                Console.WriteLine($"\nResult: {num1} {op} {num2} = {result}");
            }

            Console.Write("\nDo you want to perform another calculation? (y/n): ");
            continueCalculation = Console.ReadLine().ToLower() == "y";
        }
    }
}

using Serilog;

namespace Calculator.UI
{
    internal class InputOutput : IInputOutput
    {
        public string GetExpression()
        {   
            //check if a user input is a valid expression
            throw new NotImplementedException();
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
                    Console.WriteLine("Please enter expression: ");
                    ReadConsoleInput();
                }
            }
        }

        private string ReadConsoleInput()
        {
            string userInput = Console.ReadLine().ToLower();
            return userInput;
        }
    }
}

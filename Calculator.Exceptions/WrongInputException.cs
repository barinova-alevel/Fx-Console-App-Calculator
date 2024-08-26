namespace Calculator.Exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException() : base("The expression is invalid.") { }

        public WrongInputException(string message) : base(message) { }

        public WrongInputException(string message, Exception inner) : base(message, inner) { }
    }
}

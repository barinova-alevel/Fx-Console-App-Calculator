namespace Calculator.Exceptions
{
    public class DevideByZeroException : Exception
    {
        public DevideByZeroException() : base("Attempt to devide by zero.") { }

        public DevideByZeroException(string message) : base(message) { }

        public DevideByZeroException(string message, Exception inner) : base(message, inner) { }

    }
}

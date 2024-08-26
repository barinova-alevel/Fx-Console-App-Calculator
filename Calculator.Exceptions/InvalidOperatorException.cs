
namespace Calculator.Exceptions
{
    public class InvalidOperatorException : Exception
    {
        public InvalidOperatorException() : base() { }

        public InvalidOperatorException(string message) : base(message) { }

        public InvalidOperatorException(string message, Exception inner) : base(message, inner) { }

    }
}

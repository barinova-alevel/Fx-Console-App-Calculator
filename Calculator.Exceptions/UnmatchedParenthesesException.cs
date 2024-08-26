
namespace Calculator.Exceptions
{
    public class UnmatchedParenthesesException : Exception
    {
        public UnmatchedParenthesesException() : base("There are unmatched parentheses in the expression.") { }

        public UnmatchedParenthesesException(string message) : base(message) { }

        public UnmatchedParenthesesException(string message, Exception inner) : base(message, inner) { }
    }
}

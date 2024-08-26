
namespace Calculator.Exceptions
{
    public class OperandMissingException : Exception
    {
        public OperandMissingException() : base("An operand is missing in the expression.") { }

        public OperandMissingException(string message) : base(message) { }

        public OperandMissingException(string message, Exception inner) : base(message, inner) { }

    }
}

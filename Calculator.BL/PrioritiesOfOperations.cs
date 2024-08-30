namespace Calculator.BL
{
    public class PrioritiesOfOperations
    {
        public int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "%":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}

using System.Text.RegularExpressions;
using Serilog;

namespace Calculator.BL
{
    public class MathExpressionValidator
    {
        public bool IsValidMathExpression(string input)
        {
            if (MatchParentheses(input))
            {
                string pattern = @"^\s*[-+]?(\d+(\.\d+)?|\(\s*[-+]?\d+(\.\d+)?(\s*[-+*/%^]\s*[-+]?\d+(\.\d+)?)*\s*\))(\s*[-+*/%^]\s*[-+]?(\d+(\.\d+)?|\(\s*[-+]?\d+(\.\d+)?(\s*[-+*/%^]\s*[-+]?\d+(\.\d+)?)*\s*\)))*\s*$";

                return Regex.IsMatch(input, pattern);
            }
            return false;
        }

        private bool MatchParentheses(string input)
        {
            int parenthesesCount = 0;
            foreach (char c in input)
            {
                if (c == '(')
                    parenthesesCount++;
                else if (c == ')')
                    parenthesesCount--;

                if (parenthesesCount < 0)
                {
                    Log.Information("There are unmatched parentheses.");
                    return false;
                }
            }

            if (parenthesesCount != 0)
            {
                Log.Information("There are unmatched parentheses.");
                return false;
            }
            else
            { 
                return true;
            }
        }
    }
}

using Calculator.BL;
using System.Text.RegularExpressions;
using Serilog;

namespace Calculator.UI.Helper
{
    public class FileAnalyzer
    {
        public string _filePath { get; }
        private LineIterator _lineIterator;

        public FileAnalyzer(string filePath)
        {
            this._filePath = filePath;
        }

        public LineIterator GetIterator()
        {
            if (_lineIterator == null)
            {
                LineIterator lineIterator = new LineIterator(_filePath);
                _lineIterator = lineIterator;
                return _lineIterator;
            }
            else
            {
                return _lineIterator;
            }
        }

        public void Analyze(LineIterator lineIterator)
        {
            int indexOfCurrentLine = 0;
            string line;

            if (lineIterator != null)
            {
                do
                {
                    line = lineIterator.GetNextLine();
                    AnalyzeLine(line, indexOfCurrentLine);
                    indexOfCurrentLine++;
                }
                while (line != null);
            }
        }

        public void AnalyzeLine(string line, int lineIndex)
        {
            if (line != null && IsValidMathExpression(line))
            {
                CalculatorLogic cl = new CalculatorLogic();
                Log.Information($"Calculating line {lineIndex + 1}");
                double lineSum = cl.EvaluateExpression(line);
                Log.Information($"Result of {line}: {lineSum}");
                //add result to a file - separate method
            }
            else if (lineIndex == 0 && line == null)
            {
                Log.Information("The file is empty.");
            }
            else if (lineIndex != 0 && line == null)
            {
                Log.Information("There is no more lines.");
            }
            else
            {
                Log.Information($"Line {lineIndex + 1} {line} is not a valid math expression.");
            }
        }

        private bool IsValidMathExpression(string input)
        {
            int parenthesesCount = 0;
            foreach (char c in input)
            {
                if (c == '(')
                    parenthesesCount++;
                else if (c == ')')
                    parenthesesCount--;

                if (parenthesesCount < 0)
                    return false;
            }

            if (parenthesesCount != 0)
                return false;

            string pattern = @"^\s*[-+]?\d+(\.\d+)?(\s*[-+*/%^]\s*[-+]?\d+(\.\d+)?)*\s*$";
            return Regex.IsMatch(input, pattern);
        }
    }
}

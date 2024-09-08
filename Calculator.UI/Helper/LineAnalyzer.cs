
using Calculator.BL;
using Calculator.UI.Helper.Models;
using Serilog;
using System.Globalization;

namespace Calculator.UI.Helper
{
    public class LineAnalyzer
    {
        public LineAnalyzeResult AnalyzeLine(string line, int lineIndex)
        {
            CalculatorLogic cl = new CalculatorLogic();

                double _lineSum = cl.EvaluateExpression(line);
                Log.Information($"Reading line {line}.");
            Log.Information($"Sum of {lineIndex+1} line is {_lineSum} //message from LineAnalyzeResult");
                //bool isValid = IsNumeric(line);

                //if (isValid)
                //{
                //    _lineSum = LineSumCalculation(line);
                //}
                //else
                //{
                //    Log.Information($"Line {line} is non numeric");
                //}

                LineAnalyzeResult lineAnalyzeResult = new LineAnalyzeResult(_lineSum, lineIndex);
                return lineAnalyzeResult;
            
        }

        private double LineSumCalculation(string line)
        {
            double sum = 0.0;
            string[] numbers = line.Split(',');

            foreach (string number in numbers)
            {
                double tempNumber;
                if (double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out tempNumber))
                {
                    sum += tempNumber;
                }
                else
                {
                    Log.Debug($"Invalid number format: {number}");
                    break;
                }
            }
            return sum;
        }

        private bool IsNumeric(string line)
        {
            string[] values = line.Split(",");
            foreach (string value in values)
            {
                if (!double.TryParse(value, out double _))
                {
                    return false;
                }
            }
            return true;
        }

    }
}

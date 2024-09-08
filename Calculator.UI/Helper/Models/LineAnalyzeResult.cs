
namespace Calculator.UI.Helper.Models
{
    public class LineAnalyzeResult
    {
        //public bool IsValid;
        public double LineSum;
        public int LineIndex;

        public LineAnalyzeResult(double lineSum, int lineIndex)
        {
           // IsValid = isValid;
            LineSum = lineSum;
            LineIndex = lineIndex;
        }
    }
}


namespace Calculator.UI.Helper.Models
{
    public class FileAnalyzeResult
    {
        public int IndexOfLineWithMaxSum;
        public List<int> NonNumericLines = new List<int>();

        public FileAnalyzeResult(int maxIndex, List<int> invalidLines)
        {
            IndexOfLineWithMaxSum = maxIndex;
            NonNumericLines = invalidLines;
        }
    }
}

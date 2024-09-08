using Calculator.UI.Helper.Exceptions;
using Calculator.UI.Helper.Models;
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

        public FileAnalyzeResult Analyze(LineIterator lineIterator)
        {
            int maxIndex = 0;
            int indexOfCurrentLine = 0;
            int counterOfNumericLines = 0;
            int linesCounter = 0;
            double? maxSum = null;
            string line;
            LineAnalyzer _lineAnalyzer = new LineAnalyzer();
            List<int> invalidLines = new List<int>();

            try
            {
                if (lineIterator != null)
                {
                    do
                    {
                        line = lineIterator.GetNextLine();
                        if (line != null)
                        {
                            linesCounter++;
                        }
                        LineAnalyzeResult lineResult = _lineAnalyzer.AnalyzeLine(line, indexOfCurrentLine);

                        if (lineResult != null)
                        {
                            //if (lineResult.IsValid)
                            //{
                                if (maxSum == null)
                                {
                                    maxSum = lineResult.LineSum;
                                    maxIndex = lineResult.LineIndex;
                                }
                                else if (lineResult.LineSum > maxSum)
                                {
                                    maxSum = lineResult.LineSum;
                                    maxIndex = lineResult.LineIndex;
                                }
                                counterOfNumericLines++;
                            //}
                            //else
                            //{
                            //    Log.Information($"Adding line number {lineResult.LineIndex + 1} to non numeric.");
                            //    invalidLines.Add(lineResult.LineIndex);
                            //}
                            indexOfCurrentLine++;
                        }

                    }
                    while (line != null);
                }

                if (counterOfNumericLines == 0 && linesCounter != 0)
                {
                    throw new AllLinesNonNumericException("All lines are non numeric.");
                }
                else if (linesCounter == 0)
                {
                    throw new EmptyFileException("There is no lines to calculate.");
                }
                else
                {
                    Log.Information($"Number of line with max sum: {(maxIndex + 1)}");
                    return new FileAnalyzeResult(maxIndex, invalidLines);
                }

            }
            catch (AllLinesNonNumericException ex)
            {
                Log.Information($"There is no lines to calculate. {ex.Message}");
                return new FileAnalyzeResult(-1, invalidLines);
            }
            catch (EmptyFileException ex)
            {
                Log.Information($"{ex.Message}");
                return new FileAnalyzeResult(-1, invalidLines);
            }
        }
    }

}


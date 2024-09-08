namespace Calculator.UI.Helper
{
    public class LineIterator : IDisposable
    {
        private StreamReader _reader;
        private IEnumerator<string> _lineEnumerator;

        public LineIterator(string filePath)
        {
            _reader = new StreamReader(filePath);
            _lineEnumerator = ReadLines().GetEnumerator();
        }

        private IEnumerable<string> ReadLines()
        {
            string line;
            while ((line = _reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
        public string GetNextLine()
        {
            if (_lineEnumerator.MoveNext())
            {
                string nextLine = _lineEnumerator.Current;
                return nextLine;
            }
            else
            {
                return null;
            }
        }
        public void Dispose()
        {
            _lineEnumerator.Dispose();
            _reader.Dispose();
        }
    }
}

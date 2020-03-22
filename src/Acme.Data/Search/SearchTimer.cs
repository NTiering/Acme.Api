using System.Diagnostics;

namespace Acme.Data.Search
{
    public class SearchTimer
    {
        private readonly Stopwatch _stopwatch;

        public SearchTimer()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public long Duration => _stopwatch.ElapsedMilliseconds;
    }
}
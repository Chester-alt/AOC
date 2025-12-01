using System.Diagnostics;

namespace aoc;

public class SolutionTimer
{
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    public long ParsingElapsedTicks { get; private set; }
    public long ExecutingElapsedTicks { get; private set; }
    public long TotalElapsedTicks { get; private set; }

    public void StartParsing()
    {
        _stopwatch.Restart();
    }

    public void StartExecuting()
    {
        ParsingElapsedTicks = _stopwatch.ElapsedTicks;
    }

    public void Stop()
    {
        TotalElapsedTicks = _stopwatch.ElapsedTicks;
        ExecutingElapsedTicks = TotalElapsedTicks - ParsingElapsedTicks;
    }
}
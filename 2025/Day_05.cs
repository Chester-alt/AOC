

namespace aoc;

public class Day_05
{
    public static long Part1(SolutionTimer timer, string [] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        var ranges = new List<(long start, long end)>();
        int i = 0;
        
        // Parse the ranges until blank line  
        for (; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
                break;
            
            var parts = input[i].Split('-');
            long start  = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);
            ranges.Add((start, end));
        }
        // Parse the IDs
        var ids = new List<long>();
        for (i = i +  1; i < input.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(input[i]))
                ids.Add(long.Parse(input[i]));
        }

        int freshCount = 0;

        foreach (var id in ids)
        {
            bool isFresh = ranges.Any(r => id >= r.start && id <= r.end);
            if(isFresh)
                freshCount++;
        }
        timer.Stop();
        return freshCount;
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        return 0;
    }
}


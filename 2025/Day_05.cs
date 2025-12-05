

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
        
        // Count the fresh IDs
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
         // Merge the overlapping ranges
         ranges.Sort((a, b) => a.start.CompareTo(b.start));
         var merged = new List<(long start, long end)>();
         foreach (var r in ranges)
         {
             if (merged.Count == 0 || r.start > merged[^1].end)
             {
                 merged.Add(r);
             }
             else
             {
                 // Expand the last merged range 
                 var last = merged[^1];
                 merged[^1] = (last.start, Math.Max(last.end, r.end));
             }
         }
         // Count all IDs
         long freshCount = 0;
         foreach (var r in merged)
         {
             freshCount += (r.end - r.start + 1);
         }
         timer.Stop();
         return freshCount;
    }
}


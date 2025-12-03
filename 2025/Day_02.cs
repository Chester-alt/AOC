using System.Data.Common;

namespace aoc;

public static class Day_02
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        List<long> invalidIDs = new List<long>();

        timer.StartParsing();

        string[] ranges = input[0].Split(',');

        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            if (parts.Length != 2) continue; // Discarding odd strings 

            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            for (long id = start; id <= end; id++)
            {
                if (IsInvalid(id))
                {
                    invalidIDs.Add(id);
                }
            }

        }

        timer.Stop();

        return invalidIDs.Sum();

    }

    private static bool IsInvalid(long id)
    {
        string s = id.ToString();

        if (s.Length % 2 != 0) return false;

        int half = s.Length / 2;
        string firstHalf = s.Substring(0, half);
        string secondHalf = s.Substring(half);

        return firstHalf == secondHalf;
    }


    public static long Part2(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();

        timer.StartExecuting();
        var ans = 1;

        return ans;
        timer.Stop();
    }

}
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace aoc;

public static class Day_03  
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        long total = 0;

        foreach (var line in input)
        {
            var digits = line.Select(c => c - '0').ToArray();
            int best = 0;

            for (int i = 0; i < digits.Length; i++)
            {
                int first = digits[i];
                int second = digits.Skip(i + 1).DefaultIfEmpty(-1).Max();

                if (second == -1) continue;

                int candidate = first * 10 + second;
                if (candidate > best) best = candidate;
            }

            total += best;
        }

        return total;
    }





    public static long Part2(SolutionTimer timer, string[] input)
    {
        return 0;
    }

}
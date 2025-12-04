using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace aoc;

public static class Day_03  
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        long total = 0;

        timer.StartParsing();
        timer.StartExecuting();

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

        timer.Stop();
        return total;
    }





    public static long Part2(SolutionTimer timer, string[] input)
    {
        long total = 0;

        timer.StartParsing();
        timer.StartExecuting();

        foreach (var line in input)
        {
            int k = 12; // Max digit
            int toDrop = line.Length - k;
            var stack = new List<char>();

            foreach (char c in line)
            {
                while (stack.Count > 0 && toDrop > 0 && stack[^1] < c)
                {
                    stack.RemoveAt(stack.Count - 1);
                    toDrop--;
                }
                stack.Add(c);
            }

            while (toDrop > 0)
            {
                stack.RemoveAt(stack.Count - 1);
                toDrop--;
            }

            string best = new string(stack.Take(k).ToArray());
            total += long.Parse(best);
        }

        timer.Stop();   
        return total;      
    }

}
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization.Formatters;
using Microsoft.VisualBasic;

namespace aoc;

public static class Day_01
{
    public static int Part1(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();

        int dialPos = 50;
        int zeroCount = 0;

        foreach (var command in input)
        {

            char dir = command[0];
            int steps = int.Parse(command.AsSpan(1));

            timer.StartExecuting();
            
            if (dir == 'R')
                dialPos = (dialPos + steps) % 100;
            else if (dir == 'L')
                dialPos = (dialPos - steps + 100) % 100;
            if (dialPos == 0)
                zeroCount++;
        }
        timer.Stop();

        return zeroCount;

    }
    

    public static int Part2(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();

        int dialPos = 50;
        int zeroCount = 0;

        int CountZeroHitsRight(int s, int k)
            {
                return (s + k) / 100;  
            }

        int CountZeroHitsLeft(int s, int k)
            {
                if (s == 0) return k / 100;
                if (k < s) return 0;
                return ((k - s) / 100) + 1;
            }

        int AdvanceRight(int s, int k) => (s + k) % 100;

        int AdvanceLeft(int s, int k)
            {
                int r = k % 100;
                int n = s - r;
                return (n >= 0) ? n : n + 100;
            }

        
        foreach (var command in input)
        {

            char dir = command[0];
            int steps = int.Parse(command.AsSpan(1));

            timer.StartExecuting();

            int hitsThisCommand;
            if (dir == 'R')
            {
                hitsThisCommand = CountZeroHitsRight(dialPos, steps);
                dialPos = AdvanceRight(dialPos, steps);
            }
            else if (dir == 'L')
            {
                hitsThisCommand = CountZeroHitsLeft(dialPos, steps);
                dialPos = AdvanceLeft(dialPos, steps);
            }
            else
            {
                continue;
            }


            zeroCount += hitsThisCommand;
        }
        timer.Stop();

        return zeroCount;
    }
}
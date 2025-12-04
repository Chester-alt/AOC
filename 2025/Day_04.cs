using System.Data.Common;
using System.Runtime.CompilerServices;

namespace aoc;

public static class Day_04  
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        int accessibleCount = 0;

        timer.StartParsing();
        timer.StartExecuting();

        int[][] directions = new int[][]
        {
            new[]{-1, -1}, new[]{-1, 0}, new[]{-1, 1},
            new[]{0, -1},                 new[]{0, 1},
            new[]{1, -1},  new[]{1, 0},  new[]{1, 1}
        };


        for (int r = 0; r < input.Length; r++)
        {
            for (int c = 0; c < input[r].Length; c++)
            {
                if (input[r][c] != '@') continue;

                int neighborRolls = 0;
                foreach (var dir in directions)
                {
                    int nr = r + dir[0];
                    int nc = c + dir[1];

                    if (nr >= 0 && nr < input.Length &&
                        nc >= 0 && nc < input[nr].Length &&
                        input[nr][nc] == '@')
                    {
                        neighborRolls++;
                    }
                }

                if (neighborRolls < 4)
                    accessibleCount++;
            }
        }

        timer.Stop();   
        return accessibleCount;
    }




    public static long Part2(SolutionTimer timer, string[] input)
    {
        return 0;
    }

}
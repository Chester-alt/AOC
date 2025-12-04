using System.ComponentModel.Design;
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
        timer.StartParsing();
        timer.StartExecuting();

        // Convert to mutable grid to handle jagged rows 
        var grid = input.Select(row => row.ToCharArray()).ToArray();

        

        long totalRemoved = 0;

        while (true)
        {
            // Find all accessible rolls
            var toRemove = new List<(int r, int c)>();

            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[r].Length; c++)
                {
                    if (grid[r][c] != '@') continue;

                    int neighbors = CountNeighbors(grid, r, c, dirs);
                    if (neighbors < 4)
                    {
                        toRemove.Add((r, c));
                    }
                }
            }

            // stop if no more rolls are accessible 
            if (toRemove.Count == 0)
                break;

            // remove them in a branch and accumulate 
            foreach (var (r, c) in toRemove)
                grid[r][c] = '.';

            totalRemoved += toRemove.Count;
        }

        timer.Stop();
        return totalRemoved;
    }
    // helper to count the adjacent rolls
    private static int CountNeighbors(char[][] grid, int r, int c, int[][] dirs)
    {
        int count = 0;

        foreach (var d in dirs)
        {
            int nr = r + d[0];
            int nc = c + d[1];

            if (nr >= 0 && nr < grid.Length &&
                nc >= 0 && nc < grid[nr].Length &&
                grid[nr][nc] == '@')
            {
                count++;
            }
        }
        return count;
    }

    private static readonly int[][] dirs = new int[][]
        {
            new[]{-1, -1}, new[]{-1, 0}, new[]{-1, 1},
            new[]{0, -1},                 new[]{0, 1},
            new[]{1, -1},  new[]{1, 0},  new[]{1, 1}
        };
}
using System.Runtime.CompilerServices;
using aoc;

namespace aoc;

public class Day_07
{
    public static long Part1(SolutionTimer timer, string [] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        
        int rows = input.Length;
        int cols = input[0].Length;

       
        // Represent the grid 
        char[,] grid = new char[input.Length, input[0].Length];
        for (int r = 0; r < input.Length; r++)
        {
            for (int c = 0; c < input[r].Length; c++)
            {
                grid[r, c] = input[r][c];
            }
        }

       
        // Find the start position 
        (int startRow, int startCol) = ( - 1, - 1);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r, c] == 'S')
                {
                    startRow = r;
                    startCol = c;
                    break;
                }
            }
        }
        
        // Beam simulation 
        var queue = new Queue<(int row, int col)>();
        queue.Enqueue((startRow + 1, startCol)); // Beam starts below S
        
        var visited = new bool[rows, cols];
        long splitCount = 0;
        
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            // Continue downward until out of bounds or hitting a splitter 
            while (r < rows)
            {
                // If the cell is already part of an existing path, merge and stop
                if (visited[r, c]) break;
                
                visited[r, c] = true;
                
                if (grid[r, c] == '^')
                {
                    splitCount++;
                    
                    // Spawn left and right beams (if inside grid)
                    if (c - 1 >= 0) queue.Enqueue((r + 1, c - 1));
                    if (c + 1 < cols) queue.Enqueue((r + 1, c + 1));

                    break;
                }

                r++;
            }
        }

        timer.Stop();
        return splitCount;
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        
        timer.Stop();
        return 0;
    }
    
}
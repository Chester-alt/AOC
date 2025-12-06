using aoc;

namespace aoc;

public class Day_06
{
    public static long Part1(SolutionTimer timer, string [] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        
        // Normalise input 
        int width = input.Max(line => line.Length);
        var grid = input.Select(line => line.PadRight(width)).ToList();
        // Find the operator row 
        int opRow = grid.Count - 1;
        // Identifiy Problem column ranges 
        List<(int start, int end)> ranges = new();
        int c = 0;
        while (c < width)
        {
             // Skip separator columns
             while (c < width && grid.All(row => row[c] == ' ')) c++;
             if (c >= width) break;

             int start = c;
             while (c < width && grid.Any(row => row[c] != ' ')) c++;
             int end = c - 1;
             
             ranges.Add((start, end));
        }
        // Extract numbers and operators per problem 
        long grandTotal = 0;

        foreach (var (start, end) in ranges)
        {
            var numbers = new List<long>();
            
            // collect numbers except operator row 
            for (int r = 0; r < grid.Count; r++)
            {
                if (r == opRow) continue;
                string chunk = grid[r].Substring(start, end - start + 1).Trim();
                if (chunk.Length > 0)
                    numbers.Add(long.Parse(chunk));
            }
            // Find the operator 
            char op = ' ';
            for (int col = start; col <= end; col++)
            {
                char ch = grid[opRow][col];
                if (ch == '+' || ch == '*')
                {
                    op = ch;
                    break;
                }
            }
            // evaluate
            long result = (op == '+')
                ? numbers.Sum()
                : numbers.Aggregate(1L, (acc, n) => acc * n);
            
            grandTotal += result;
        }
        
        
        
        timer.Stop();
        return grandTotal;
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        
        timer.Stop();
        return 0;
    }
    
}
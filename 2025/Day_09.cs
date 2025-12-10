namespace aoc;

public class Day_09
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();
        // Parse input into points
        var pts = new List<(int X, int Y)>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            pts.Add((x, y));
        }
        long bestArea = 0;
         
        timer.StartExecuting();
        // Brute-force all pairs
        for (int i = 0; i < pts.Count; i++)
        {
            for (int j = i + 1; j < pts.Count; j++)
            {
                var a = pts[i];
                var b = pts[j];

                long width = Math.Abs(a.X - b.X) + 1;
                long height = Math.Abs(a.Y - b.Y) + 1;
                long area = width * height;

                if (area > bestArea)
                    bestArea = area;
            }
        }
        timer.Stop();
        return bestArea;
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        return 0;
    }
}
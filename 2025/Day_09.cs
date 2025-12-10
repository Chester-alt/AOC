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
        // Parse red points
        var reds = new List<(int X, int Y)>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            reds.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        // Coordinate compression
        var xsSet = new SortedSet<int>();
        var ysSet = new SortedSet<int>();
        foreach (var (x, y) in reds)
        {
            xsSet.Add(x);
            ysSet.Add(y);
            xsSet.Add(x + 1);
            ysSet.Add(y + 1);
        }

        var xs = xsSet.ToArray();
        var ys = ysSet.ToArray();
        var xIndex = new Dictionary<int, int>();
        var yIndex = new Dictionary<int, int>();
        for (int i = 0; i < xs.Length; i++) xIndex[xs[i]] = i;
        for (int i = 0; i < ys.Length; i++) yIndex[ys[i]] = i;

        // Mark boundary segments
        var allowed = new bool[xs.Length, ys.Length];
        foreach (var (x, y) in reds)
            allowed[xIndex[x], yIndex[y]] = true;

        for (int i = 0; i < reds.Count; i++)
        {
            var (x1, y1) = reds[i];
            var (x2, y2) = reds[(i + 1) % reds.Count];
            int xi1 = xIndex[x1], yi1 = yIndex[y1];
            int xi2 = xIndex[x2], yi2 = yIndex[y2];

            if (y1 == y2)
            {
                int a = Math.Min(xi1, xi2), b = Math.Max(xi1, xi2);
                for (int xi = a; xi <= b; xi++) allowed[xi, yi1] = true;
            }
            else
            {
                int a = Math.Min(yi1, yi2), b = Math.Max(yi1, yi2);
                for (int yi = a; yi <= b; yi++) allowed[xi1, yi] = true;
            }
        }

        // Fill interior (scanline)
        for (int yi = 0; yi < ys.Length; yi++)
        {
            var cols = new List<int>();
            for (int xi = 0; xi < xs.Length; xi++)
                if (allowed[xi, yi])
                    cols.Add(xi);
            if (cols.Count < 2) continue;
            cols.Sort();
            for (int k = 0; k + 1 < cols.Count; k += 2)
            {
                int left = cols[k], right = cols[k + 1];
                for (int xi = left; xi <= right; xi++)
                    allowed[xi, yi] = true;
            }
        }

        // Build weighted prefix sums
        var dx = new int[xs.Length - 1];
        for (int i = 0; i < dx.Length; i++) dx[i] = xs[i + 1] - xs[i];
        var dy = new int[ys.Length - 1];
        for (int i = 0; i < dy.Length; i++) dy[i] = ys[i + 1] - ys[i];

        var w = new long[dx.Length, dy.Length];
        for (int xi = 0; xi < dx.Length; xi++)
        for (int yi = 0; yi < dy.Length; yi++)
        {
            if (allowed[xi, yi])
                w[xi, yi] = (long)dx[xi] * dy[yi];
        }

        var ps = new long[dx.Length + 1, dy.Length + 1];
        for (int xi = 0; xi < dx.Length; xi++)
        for (int yi = 0; yi < dy.Length; yi++)
        {
            ps[xi + 1, yi + 1] = ps[xi, yi + 1] + ps[xi + 1, yi] - ps[xi, yi] + w[xi, yi];
        }

        long RectSum(int x0, int y0, int x1, int y1)
        {
            return ps[x1, y1] - ps[x0, y1] - ps[x1, y0] + ps[x0, y0];
        }

        // Brute-force red pairs
        long best = 0;
        for (int i = 0; i < reds.Count; i++)
        {
            for (int j = i + 1; j < reds.Count; j++)
            {
                var (x1, y1) = reds[i];
                var (x2, y2) = reds[j];
                long area = (Math.Abs(x1 - x2) + 1L) * (Math.Abs(y1 - y2) + 1L);

                int xi0 = Math.Min(xIndex[x1], xIndex[x2]);
                int xi1 = Math.Max(xIndex[x1], xIndex[x2]);
                int yi0 = Math.Min(yIndex[y1], yIndex[y2]);
                int yi1 = Math.Max(yIndex[y1], yIndex[y2]);

                long covered = RectSum(xi0, yi0, xi1 + 1, yi1 + 1);
                if (covered == area && area > best)
                    best = area;
            }
        }
        return best;

    }
} 
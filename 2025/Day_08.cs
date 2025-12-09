using System.Runtime.CompilerServices;
using aoc;

namespace aoc;

public class Day_08
{
    public static long Part1(SolutionTimer timer, string [] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        // Parse the input
        var points = input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var parts = line.Split(',');
                return (X: int.Parse(parts[0]),
                    Y: int.Parse(parts[1]),
                    Z: int.Parse(parts[2]));
            })
            .ToList();
        // Build all pair distances 
        
        var edges = new List<(int A, int B, long Dist2)>();
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                long dx = points[j].X - points[i].X;
                long dy = points[j].Y - points[i].Y;
                long dz = points[j].Z - points[i].Z;
                long d2 = dx * dx + dy * dy + dz * dz;
                edges.Add((i, j, d2));
            }
        }
        // edges.Sort((a, b) => a.Dist2.CompareTo(b.Dist2));
    
        // Union-find for circuits 
        var parent = Enumerable.Range(0, points.Count).ToArray();
        var size = Enumerable.Repeat(1, points.Count).ToArray();

        int Find(int x)
        {
            if (parent[x] != x) parent[x] = Find(parent[x]);
            return parent[x];
        }

        void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);
            if (a == b) return; 
            if (size[a] < size[b]) (a, b) = (b, a);
            parent[b] = a;
            size[a] += size[b];
        }
        
        // Only union the 1000 shortest edges
        foreach (var edge in edges.OrderBy(e => e.Dist2).Take(1000))
        {
            Union(edge.A, edge.B);
        }

        var compSizes = new Dictionary<int, int>();
        for (int i = 0; i < points.Count; i++)
        {
            int r = Find(i);
            if (!compSizes.ContainsKey(r)) compSizes[r] = 0;
            compSizes[r]++;
        }
        
        // Multiply the top 3 sizes 
        var top3 = compSizes.Values
            .OrderByDescending(x => x)
            .Take(3)
            .ToArray();
        
        long results = top3.Aggregate(1L, (acc, v) => acc * v);

        timer.Stop();
        return results;
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        timer.StartParsing();
        timer.StartExecuting();
        
        timer.Stop();
        return 0;
    }
    
}
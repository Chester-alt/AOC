namespace aoc;

public class Day_10
{
    public static long Part1(SolutionTimer timer, string[] input)
    {
        long totalPresses = 0;

    foreach (var line in input)
    {
        
        // --- Parse target pattern ---
        int startIdx = line.IndexOf('[');
        int endIdx = line.IndexOf(']');
        string pattern = line.Substring(startIdx + 1, endIdx - startIdx - 1);
        int nLights = pattern.Length;

        int target = 0;
        for (int i = 0; i < nLights; i++)
        {
            if (pattern[i] == '#')
                target |= (1 << i);
        }

        // --- Parse buttons ---
        var buttons = new List<int[]>();
        int pos = 0;
        while ((pos = line.IndexOf('(', pos)) != -1)
        {
            int close = line.IndexOf(')', pos);
            string inside = line.Substring(pos + 1, close - pos - 1);
            if (!string.IsNullOrWhiteSpace(inside))
            {
                var indices = inside.Split(',')
                                    .Select(s => int.Parse(s.Trim()))
                                    .ToArray();
                buttons.Add(indices);
            }
            pos = close + 1;
        }

        // --- BFS to find minimum presses ---
        int presses = MinPresses(nLights, target, buttons);
        totalPresses += presses;
    }

    return totalPresses;
}

// Helper: toggle lights according to button
static int Toggle(int state, int[] button)
{
    foreach (var idx in button)
    {
        state ^= (1 << idx); // flip bit
    }
    return state;
}

// BFS search
static int MinPresses(int nLights, int target, List<int[]> buttons)
{
    int start = 0; // all off
    var queue = new Queue<(int state, int steps)>();
    var seen = new HashSet<int>();

    queue.Enqueue((start, 0));
    seen.Add(start);

    while (queue.Count > 0)
    {
        var (state, steps) = queue.Dequeue();
        if (state == target) return steps;

        foreach (var btn in buttons)
        {
            int next = Toggle(state, btn);
            if (!seen.Contains(next))
            {
                seen.Add(next);
                queue.Enqueue((next, steps + 1));
            }
        }
    }
    
    return -1; // no solution
    }

    public static long Part2(SolutionTimer timer, string[] input)
    {
        long totalPresses = 0;

    foreach (var line in input)
    {
        // --- Parse joltage requirements ---
        int startIdx = line.IndexOf('{');
        int endIdx = line.IndexOf('}');
        string reqs = line.Substring(startIdx + 1, endIdx - startIdx - 1);
        int[] target = reqs.Split(',')
                           .Select(s => int.Parse(s.Trim()))
                           .ToArray();
        int nCounters = target.Length;

        // --- Parse buttons ---
        var buttons = new List<int[]>();
        int pos = 0;
        while ((pos = line.IndexOf('(', pos)) != -1)
        {
            int close = line.IndexOf(')', pos);
            string inside = line.Substring(pos + 1, close - pos - 1);
            if (!string.IsNullOrWhiteSpace(inside))
            {
                var indices = inside.Split(',')
                                    .Select(s => int.Parse(s.Trim()))
                                    .ToArray();
                buttons.Add(indices);
            }
            pos = close + 1;
        }

        // --- BFS to find minimum presses ---
        int presses = MinPressesCounters(target, buttons);
        totalPresses += presses;
    }

    return totalPresses;
}

// BFS for counters
static int MinPressesCounters(int[] target, List<int[]> buttons)
{
    var start = new int[target.Length]; // all zero
    var queue = new Queue<(int[] state, int steps)>();
    var seen = new HashSet<string>();

    queue.Enqueue((start, 0));
    seen.Add(string.Join(",", start));

    while (queue.Count > 0)
    {
        var (state, steps) = queue.Dequeue();

        // check if we reached target
        bool done = true;
        for (int i = 0; i < target.Length; i++)
            if (state[i] != target[i]) { done = false; break; }
        if (done) return steps;

        // try pressing each button
        foreach (var btn in buttons)
        {
            var next = (int[])state.Clone();
            foreach (var idx in btn)
                next[idx]++;

            // prune: if any counter exceeds target, skip
            bool valid = true;
            for (int i = 0; i < target.Length; i++)
                if (next[i] > target[i]) { valid = false; break; }
            if (!valid) continue;

            string key = string.Join(",", next);
            if (!seen.Contains(key))
            {
                seen.Add(key);
                queue.Enqueue((next, steps + 1));
            }
        }
    }

    return -1; // no solution
    }
}
using aoc;

var selectedDay = 1;
var selectedPart = 1;
var shouldBench = true;

var solutions = new Func<SolutionTimer, string[], int>[][]
{
    [
        Day_01.Part1,
        Day_01.Part2,
    ],
};

var input = File.ReadAllLines("input.txt");

var action = solutions[selectedDay - 1][selectedPart - 1];
Console.WriteLine($"Executing day {selectedDay} part {selectedPart}");
if (shouldBench)
{
    try
    {
        var timer = new SolutionTimer();

        const int warmupIterations = 10_000;
        const int iterations = 10_000;

        for (int i = 0; i < warmupIterations; i++)
            action(timer, input);

        long parsingTicks = 0;
        long executingTicks = 0;
        long totalTicks = 0;

        for (int i = 0; i < iterations; i++)
        {
            action(timer, input);
            parsingTicks += timer.ParsingElapsedTicks;
            executingTicks += timer.ExecutingElapsedTicks;
            totalTicks += timer.TotalElapsedTicks;
        }

        var parseMs = parsingTicks / (float)TimeSpan.TicksPerMillisecond / iterations;
        var execMs  = executingTicks / (float)TimeSpan.TicksPerMillisecond / iterations;
        var totalMs = totalTicks / (float)TimeSpan.TicksPerMillisecond / iterations;

        Console.WriteLine($"Result: {action(timer, input)}");
        Console.WriteLine($"Parsing: {parseMs}ms");
        Console.WriteLine($"Executing: {execMs}ms");
        Console.WriteLine($"Total: {totalMs}ms");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}
else
{
    try
    {
        var timer = new SolutionTimer();
        Console.WriteLine($"Result: {action(timer, input)}");

        var parseMs = timer.ParsingElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
        var execMs  = timer.ExecutingElapsedTicks / (float)TimeSpan.TicksPerMillisecond;
        var totalMs = timer.TotalElapsedTicks / (float)TimeSpan.TicksPerMillisecond;

        Console.WriteLine($"Parsing: {parseMs}ms");
        Console.WriteLine($"Executing: {execMs}ms");
        Console.WriteLine($"Total: {totalMs}ms");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

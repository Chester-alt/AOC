using System.Diagnostics;

var watch = System.Diagnostics.Stopwatch.StartNew();

try
{
    using StreamReader reader = new("200wordsample.txt");

    string text = reader.ReadToEnd();

    Console.WriteLine(text);
}
catch (IOException e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}

watch.Stop();

var elapsedMs = watch.ElapsedMilliseconds;

Console.WriteLine($"Time Taken {elapsedMs} ms");
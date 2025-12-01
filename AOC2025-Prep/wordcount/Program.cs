using System.ComponentModel;
using System.Diagnostics;

var watch = System.Diagnostics.Stopwatch.StartNew();

try
{
    using var reader = new StreamReader("9kwordsample.txt");

    var stats = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    char[] separators = { ' ', ',', '.', ':', '?', ';', '!', '\n', '\r', '\t' };
    
    int minWordLength = 2;

    string? line;
    while ((line = reader.ReadLine()) != null)
    {
       string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            if (word.Length >= minWordLength)
            {
                // Avoiding double lookups 
                if (stats.TryGetValue(word, out int count))
                    stats[word] = count + 1;
                else
                    stats[word] = 1;
            }
        }
    }
    
    // Order the list by word occurrence 
    var orderedStats = stats.OrderByDescending(x => x.Value);
    // Print total word count 
    Console.WriteLine($"Total unique word count: {stats.Count}"); 
    // Print the occurrences of each word 
    foreach (var pair in orderedStats.Take(10))
    {
        Console.WriteLine($"Total occurrences of {pair.Key}: {pair.Value}");
    }
}
catch (IOException e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}

watch.Stop();

var elapsedMs = watch.ElapsedMilliseconds;

Console.WriteLine($"Time Taken {elapsedMs} ms");
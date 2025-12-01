using System.ComponentModel;
using System.Diagnostics;

var watch = System.Diagnostics.Stopwatch.StartNew();

try
{
    using var reader = new StreamReader("200wordsample.txt");

    var stats = new Dictionary<string, int>();
    char[] seperators = { ' ', ',', '.', ':', '?', ';', '!', '\n', '\r', '\t' };
    
    int minWordLength = 2;

    string? line;
    while ((line = reader.ReadLine()) != null)
    {
       string[] words = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            string w = word.Trim().ToLower();
            if (w.Length > minWordLength)
            {
                // Avoiding double lookups 
                if (stats.TryGetValue(w, out int count))
                    stats[w] = count + 1;
                else
                    stats[w] = 1;
            }
        }
    }
    
    // Order the list by word occurrence 
    var orderedStats = stats.OrderByDescending(x => x.Value);
    // Print total word count 
    Console.WriteLine("Total word count: {0}", stats.Count); 
    // Print the occurrences of each word 
    foreach (var pair in orderedStats.Take(10))
    {
        Console.WriteLine("Total occurrences of {0}: {1}", pair.Key, pair.Value);
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
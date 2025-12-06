using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

var watch = System.Diagnostics.Stopwatch.StartNew();

try
{
    using var reader = new StreamReader("20mwordsample.txt");

    //var stats = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    //char[] separators = { ' ', ',', '.', ':', '?', ';', '!', '\n', '\r', '\t' };
    
    int minWordLength = 2;
    var stats = new ConcurrentDictionary<string, int>(StringComparer.Ordinal);
    
    // Partition the file into chunks of lines
    Parallel.ForEach(File.ReadLines("20mwordsample.txt"),
        () => new Dictionary<string, int>(StringComparer.Ordinal),
        (line, state, localDict) =>
        {
            ReadOnlySpan<char> span = line.AsSpan();
            int start = -1;

            for (int i = 0; i < span.Length; i++)
            {
                char c = span[i];

                if (char.IsLetter(c))
                {
                    if (start == -1) start = i;
                }
                else
                {
                    if (start != -1)
                    {
                        var wordSpan = span.Slice(start, i - start);
                        if (wordSpan.Length >= minWordLength)
                        {
                            string word = wordSpan.ToString().ToLowerInvariant();
                            if (localDict.TryGetValue(word, out int count))
                                localDict[word] = count + 1;
                            else
                                localDict[word] = 1;
                        }

                        start = -1;
                    }
                }
            }
            // Handle word at end of line
            if (start != -1)
            {
                var wordSpan = span.Slice(start, span.Length - start);
                if (wordSpan.Length >= minWordLength)
                {
                    string word = wordSpan.ToString().ToLowerInvariant();
                    if (localDict.TryGetValue(word, out int count))
                        localDict[word] = count + 1;
                    else
                        localDict[word] = 1;
                }
            }

            return localDict;
        },
        localDict =>
        {
            // Merge thread-local results into global ConcurrentDictionary
            foreach (var kvp in localDict)
                stats.AddOrUpdate(kvp.Key, kvp.Value, (_, old) => old + kvp.Value);
        });

    
    
    
    // Print total word count 
    Console.WriteLine($"Total unique word count: {stats.Count}"); 
    // Print the occurrences of each word 
    foreach (var pair in stats.OrderByDescending(x => x.Value).Take(10))
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
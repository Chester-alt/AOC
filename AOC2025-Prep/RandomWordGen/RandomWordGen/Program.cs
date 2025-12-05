using System;
using System.IO;
using System.Linq;
using System.Diagnostics;



class Program
{
    static void Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        int wordCount = 20000000;
        string inputFile = "words.txt";
        string outputFile = "output.txt";

        // read all words from file 
        string[] allWords = File.ReadAllLines(inputFile);
        
        Random random = new Random();
        
        // Pick random words 
        var selectedWords = Enumerable.Range(0, wordCount)
            .Select(_ => allWords[random.Next(0, allWords.Length)])
            .ToArray();
        // Write all the words to a file 
        using (StreamWriter writer = new StreamWriter(outputFile))
            for (int i = 0; i < selectedWords.Length; i += 100)
            {
                var chunk = selectedWords.Skip(i).Take(100);
                
                string line = string.Join(" ", chunk);
                
                writer.WriteLine(line);
            }
        
        Console.WriteLine($"Created {wordCount} words in {outputFile}");



    watch.Stop();

    var elapsedMs = watch.ElapsedMilliseconds;

    Console.WriteLine($"Time Taken {elapsedMs} ms");

    }
}
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

//Just to Check performance.
Stopwatch sw = new Stopwatch();

sw.Start();

//input file should have the name of input.txt and the file need to be in the same directory with the program.
string inputFilePath = "input.txt";
string outputFilePath = "output.txt";
string outputFileForApprouchPath = "outputWithFor.txt";

Regex regex = new Regex(@"[^a-zA-Z]+");

//Reading the txt file and spliting the words.
var words = File.ReadLines(inputFilePath)
                .SelectMany(line => regex.Split(line.ToLower()))
                .Where(word => !string.IsNullOrEmpty(regex.Replace(word, "")))
                .GroupBy(word => word)
                .OrderBy(group => group.Key) //comment to show the performance difference
                .ToList();

//Counting and Writing the words to the output file.
File.WriteAllLines(outputFilePath, words.Select(w => $"{w.Key} - {w.Count()}"));

var wordss = File.ReadLines(inputFilePath)
    .SelectMany(line => regex.Split(line.ToLower()))
    .Where(word => !string.IsNullOrEmpty(regex.Replace(word, "")));

//Double-checking the total word count.
File.WriteAllLines("test.txt", words.SelectMany(line => line)
                                    .GroupBy(word => word)
                                    .ToList()
                                    .Select(line => line.Count().ToString()));

//stop the stopwatch
sw.Stop();

//log the time spend to the console
Console.WriteLine("| ####### Results Using Lambda Approuch ####### |");
Console.WriteLine($"|        Time to Run(sec):       | Total Words: |");
Console.WriteLine($"|            {Math.Round(sw.Elapsed.TotalSeconds,5)}             |    {words
                                                                                            .SelectMany(line => line)
                                                                                            .GroupBy(word => word)
                                                                                            .ToList()
                                                                                            .Select(line => line.Count()).Sum()}    |");
Console.WriteLine("| ############################################# |");
Console.WriteLine("--");

//Secound Approach
sw.Reset();
sw.Start();

//same task using foreach loop
var wordsFromInputFile = File.ReadLines(inputFilePath);
Dictionary<string, int> wordCount = new Dictionary<string, int>();

foreach (var line in wordsFromInputFile)
{
    var Lines = regex.Split(line.ToLower());

    foreach (var word in Lines)
    {
        var currentWord = word.ToLower();
        if (!string.IsNullOrEmpty(regex.Replace(word, "")))
        {
            if (wordCount.ContainsKey(currentWord)){
                wordCount[currentWord]++;
            }
            else{
                wordCount.Add(currentWord, 1);
            }
            var wordFor = word;
        }
    }
}

File.WriteAllLines(outputFileForApprouchPath, wordCount.Select(w => $"{w.Key} - {w.Value}").OrderBy(w => w));
sw.Stop();

Console.WriteLine("| ###### Results Using foreach Approuch ####### |");
Console.WriteLine($"|        Time to Run(sec):      | Total Words:  |");
Console.WriteLine($"|            {Math.Round(sw.Elapsed.TotalSeconds,5)}            |  {wordCount.Sum(w => w.Value)}       |");
Console.WriteLine("| ############################################# |");
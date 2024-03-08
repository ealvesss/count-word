using System.Diagnostics;
using System.Text.RegularExpressions;

//Just to Check performance.
Stopwatch sw = new Stopwatch();

sw.Start();

//input file should have the name of input.txt and the file need to be in the same directory with the program.
string inputFilePath = "input.txt"; 
string outputFilePath = "output.txt";
Regex regex = new Regex(@"[^a-zA-Z]+");

//Reading the txt file and spliting the words.
var words = File.ReadLines(inputFilePath)
    .SelectMany(line => regex.Split(line.ToLower()))
    .Where(word => !string.IsNullOrEmpty(regex.Replace(word, "")))
    .GroupBy(word => word)
    .OrderBy(group => group.Key)
    .ToList();

//Counting and Writing the words to the output file.
File.WriteAllLines(outputFilePath, words.Select(w => $"{w.Key} - {w.Count()}"));

//stop the stopwatch
sw.Stop();

//log the time spend to the console
Console.WriteLine("Spend Time (ms): " + sw.Elapsed.TotalSeconds);
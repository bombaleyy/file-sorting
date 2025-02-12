using System;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // What base folder to check
        Console.Write("Enter the path to the base folder you want to check: ");
        string? folderPath = Console.ReadLine();

        // Where I want the output to be
        Console.Write("Enter the path to where you want the text file to be stored: ");
        string? outputFolderPath = Console.ReadLine();

        // What base folder to check
        Console.Write("Enter the name you want to give to the text file: ");
        string? outputFileName = Console.ReadLine();

        if (!Directory.Exists(folderPath) || !Directory.Exists(outputFolderPath))
        {
            Console.WriteLine("The specified folder does not exist.");
            return;
        }

        // Get all files recursively
        var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
                             .Select(f => new FileInfo(f))
                             .OrderByDescending(f => f.Length) // Sort by size (descending)
                             .ToList();

        string outputFile = Path.Combine(outputFolderPath, $"{outputFileName}.txt");

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (var file in files)
            {
                double sizeInGB = file.Length / 1073741824.0; // Convert bytes to GB
                writer.WriteLine($"{sizeInGB.ToString("0.000", CultureInfo.InvariantCulture)} GB - \"{file.Name}\" - \"{file.FullName}\"");
                writer.WriteLine();
            }
        }

        Console.WriteLine($"File list saved to: {outputFile}");
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine(); // Keeps the console open

    }
}
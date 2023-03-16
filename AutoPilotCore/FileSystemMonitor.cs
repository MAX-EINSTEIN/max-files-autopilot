using System.IO;

namespace AutoPilot.Core;

public class FileSystemMonitor
{
    public static void WatchDirectory(string path)
    {
        System.Console.WriteLine($"Monitoring the File System ({path}) for changes...");
    }

    public static void OpenFile(string filePath)
    {
        if (!IsBinary(filePath))
        {
            System.Console.WriteLine("INFO: Opening file: \"{0}\"\n", filePath);
            var content = File.ReadAllLines(filePath);
            foreach (var line in content)
                System.Console.WriteLine(line);
        }
        else
        {
            System.Console.WriteLine("INFO: Cannot open binary file: \"{0}\"", filePath);
        }
    }

    private static bool IsBinary(string filePath, int requiredConsecutiveNulls = 1)
    {
        const int charsToCheck = 8000;
        const char nullChar = '\0';

        int nullCount = 0;

        using (var streamReader = new StreamReader(filePath))
        {
            for (int i = 0; i < charsToCheck; i++)
            {
                if (streamReader.EndOfStream) return false;

                if ((char)streamReader.Read() == nullChar)
                {
                    nullCount++;
                    if (nullCount >= requiredConsecutiveNulls) return true;
                }
                else
                {
                    nullCount = 0;
                }

            }
        }

        return false;
    }
}



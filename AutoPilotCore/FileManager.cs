namespace AutoPilot.Core;

public class FileManager
{

    public static void OpenFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            System.Console.WriteLine("ERROR: Can not found file: {0}", filePath);
            return;
        }

        if (!IsBinary(filePath))
        {
            System.Console.WriteLine("INFO: Opening file: \"{0}\"\n", filePath);
            var content = File.ReadAllLines(filePath);
            foreach (var line in content)
                System.Console.WriteLine(line);
        }
        else
        {
            System.Console.WriteLine("INFO: Can not open binary file: \"{0}\"", filePath);

            System.Console.WriteLine("INFO: Attempting to open the file using the associated default program... ");

            if (OpenWithDefaultProgram(filePath))
            {
                System.Console.WriteLine("INFO: File opened with the default program.");
            }
            else
            {
                System.Console.WriteLine("ERROR: Can't open file: {0}", filePath);
            }
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

    private static bool OpenWithDefaultProgram(string filePath)
    {
        bool openProcessStatus = false;

        try
        {
            using var fileopener = new System.Diagnostics.Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + filePath + "\"";
            fileopener.Start();

            openProcessStatus = true;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("LOG: {0}", ex.Message);
            return false;
        }

        return openProcessStatus;
    }

}

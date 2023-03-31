namespace AutoPilot.Core;

public class FileSystemMonitor
{
    public static void WatchDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new Exception("Please make sure the directory exists.");
        }

        using var fileSystemWatcher = new FileSystemWatcher(path);

        fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

        fileSystemWatcher.Changed += OnChanged;
        fileSystemWatcher.Created += OnCreated;
        fileSystemWatcher.Deleted += OnDeleted;
        fileSystemWatcher.Renamed += OnRenamed;
        fileSystemWatcher.Error += OnError;

        // fileSystemWatcher.Filter = "*.*";
        // watcher.Filter = "*.txt";
        // watcher.IncludeSubdirectories = true;
        fileSystemWatcher.EnableRaisingEvents = true;

        System.Console.WriteLine($"Monitoring the File System Folder: ({path}) for changes...");

        System.Console.WriteLine("Press any key to stop watching changes and exit...");
        Console.ReadLine();
    }

    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed)
        {
            return;
        }
        Console.WriteLine($"Changed: {e.FullPath}");
    }

    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        string value = $"Created: {e.FullPath}";
        Console.WriteLine(value);
    }

    private static void OnDeleted(object sender, FileSystemEventArgs e) =>
        Console.WriteLine($"Deleted: {e.FullPath}");

    private static void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"Renamed:");
        Console.WriteLine($"    Old: {e.OldFullPath}");
        Console.WriteLine($"    New: {e.FullPath}");
    }

    private static void OnError(object sender, ErrorEventArgs e) =>
        PrintException(e.GetException());

    private static void PrintException(Exception? ex)
    {
        if (ex != null)
        {
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine("Stacktrace:");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();
            PrintException(ex.InnerException);
        }
    }

}



namespace AutoPilot.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        Core.FileSystemMonitor.WatchDirectory("C:/Users/default/Downloads");
    }
}

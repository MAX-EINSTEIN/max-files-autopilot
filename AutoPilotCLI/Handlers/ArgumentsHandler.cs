using CommandLine;

namespace AutoPilot.CLI;

public static class ArgumentsHandler
{
    public static void Initialize(string[] args)
    {
        MonitorOptions monitorOptions = new MonitorOptions();

        CommandLine.Parser.Default
        .ParseArguments<MonitorOptions, OpenOptions>(args)
        .MapResult(
            (MonitorOptions opts) => HandleMonitorOptions(opts),
            (OpenOptions opts) => HandleOpenOptions(opts),
            errs => HandleParseErrors(errs)
        );
    }

    private static int HandleMonitorOptions(MonitorOptions opts)
    {
        // handle options
        if (opts.DirectoriesToMonitor == null) return (int)HandlerResults.FAILURE;

        var directory = opts.DirectoriesToMonitor.ToList().First();
        Core.FileSystemMonitor.WatchDirectory(directory);

        return (int)HandlerResults.SUCCESS;
    }


    private static int HandleOpenOptions(OpenOptions opts)
    {
        if (opts.FileToOpen != null)
            Core.FileManager.OpenFile(opts.FileToOpen);

        return ((int)HandlerResults.SUCCESS);
    }


    private static int HandleParseErrors(IEnumerable<Error> errs)
    {
        //handle errors
        System.Console.WriteLine("LOG(S):");
        foreach (var err in errs)
            System.Console.WriteLine(err);
        return ((int)HandlerResults.SUCCESS);
    }
}

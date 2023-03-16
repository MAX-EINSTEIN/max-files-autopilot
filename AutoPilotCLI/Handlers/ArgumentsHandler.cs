using CommandLine;

namespace AutoPilot.CLI;

public static class ArgumentsHandler
{
    public static void Initialize(string[] args)
    {
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
        if (opts.DirectoriesToMonitor != null)
        {
            foreach (var directory in opts.DirectoriesToMonitor)
                Core.FileSystemMonitor.WatchDirectory(directory);
        }

        return 1;
    }


    private static int HandleOpenOptions(OpenOptions opts)
    {
        if (opts.FileToOpen != null)
            Core.FileSystemMonitor.OpenFile(opts.FileToOpen);

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

using CommandLine;

namespace AutoPilot.CLI;

[Verb("monitor", HelpText = "Directory to watch for changes.")]
class MonitorOptions
{
    [Option('p', "path", Required = true, HelpText = "Complete path of the directory to watch.")]
    public IEnumerable<string>? DirectoriesToMonitor { get; set; }
}

[Verb("open", HelpText = "Opens a file with the associated default program or prints it.")]
class OpenOptions
{
    [Option('p', "path", Required = true, HelpText = "Complete path of the file to open.")]
    public string? FileToOpen { get; set; }
}

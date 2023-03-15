using CommandLine;

namespace AutoPilot.CLI;

[Verb("monitor", HelpText = "Directory to watch for changes.")]
class MonitorOptions
{
    [Option('p', "path", Required = true, HelpText = "File path of the directory to watch")]
    public IEnumerable<string>? DirectoriesToMonitor { get; set; }
}

[Verb("open", HelpText = "Opens a file with the associated default program or prints it.")]
class OpenOptions
{
    [Option('n', "name", Required = true, HelpText = "Name with complete path to the file")]
    public string? FileToOpen { get; set; }
}

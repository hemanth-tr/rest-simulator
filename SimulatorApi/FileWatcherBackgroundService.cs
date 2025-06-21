using Microsoft.Extensions.FileSystemGlobbing;

namespace SimulatorApi;

public class FileWatcherBackgroundService : BackgroundService
{
    private readonly string DataDirectory = "data";
    public FileWatcherBackgroundService(ILogger<FileWatcherBackgroundService> logger)
    {
        this.Logger = logger;
        DataDirectory = AppDomain.CurrentDomain.BaseDirectory + DataDirectory;
    }

    private ILogger<FileWatcherBackgroundService> Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.Logger.LogInformation($"Started StubFileWatcher");
        this.Logger.LogInformation($"DirectoryExists: {Directory.Exists(DataDirectory)}");
        this.Logger.LogInformation($"Looking for files in {DataDirectory}");

        using var watcher = new FileSystemWatcher
        {
            // Set the path to monitor (e.g., a directory)
            Path = DataDirectory,
            // Monitor all files (you can specify a filter, e.g., "*.txt")
            Filter = "*.*",
            IncludeSubdirectories = false,
            EnableRaisingEvents = true
        };

        // Subscribe to events
        this.Logger.LogInformation($"Subscribing to file events");
        watcher.Changed += OnChanged;
        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Renamed += OnRenamed;

        try
        {
            // Wait for cancellation while keeping the service alive
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            Logger.LogInformation("FileSystemWatcher service is stopping...");
        }
        finally
        {
            // Clean up FileSystemWatcher
            watcher?.Dispose();
        }
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        this.Logger.LogInformation($"Renamed: {e.OldName} to {e.Name}");
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        this.Logger.LogInformation($"Deleted: {e.Name}");
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        this.Logger.LogInformation($"Created: {e.Name}");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        this.Logger.LogInformation($"Changed: {e.Name}");
    }
}
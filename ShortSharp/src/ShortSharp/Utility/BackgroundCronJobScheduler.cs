using Timer = System.Timers.Timer;

namespace ShortSharp.Utility;

/// <summary>
/// Simple, Basic, Elegant, InMemory implementation of job scheduler
/// </summary>
public class BackgroundCronJobScheduler : IDisposable
{
    private static BackgroundCronJobScheduler? _instance;
    public static BackgroundCronJobScheduler Instance => _instance ??= new BackgroundCronJobScheduler();
    public readonly IDictionary<DateTime, Timer> ScheduledTasks = new Dictionary<DateTime, Timer>();

    private BackgroundCronJobScheduler()
    {
    }

    public void ScheduleNew(Action jobFunction, int crownIntervalInMinutes)
    {
        //run first time when start
        jobFunction.Invoke();

        //invoke crown jobFunction after interval
        using var timer = new Timer(TimeSpan.FromMinutes(crownIntervalInMinutes).TotalMilliseconds);
        timer.Elapsed += (sender, eventArgs) => { jobFunction.Invoke(); };
        timer.Start();

        ScheduledTasks.Add(DateTime.UtcNow, timer);
    }

    public void Clear(DateTime dateTime)
    {
        if (!ScheduledTasks.ContainsKey(dateTime))
        {
            throw new InvalidDataException();
        }

        var task = ScheduledTasks[dateTime];
        task.Stop();
        task.Close();
        task.Dispose();
        ScheduledTasks.Clear();
    }

    public void ClearAll()
    {
        foreach (var task in ScheduledTasks)
        {
            task.Value.Stop();
            task.Value.Close();
            task.Value.Dispose();
        }
    }

    public void Dispose()
    {
        try
        {
            ClearAll();
            GC.SuppressFinalize(this);
        }
        catch
        {
            // ignored
        }
    }
}
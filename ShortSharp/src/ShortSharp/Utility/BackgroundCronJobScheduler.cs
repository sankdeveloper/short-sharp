using Timer = System.Timers.Timer;

namespace ShortSharp.Utility;

/// <summary>
/// Simple, Basic, Elegant, InMemory implementation of job scheduler
/// </summary>
public class BackgroundCronJobScheduler : IDisposable
{
    private static BackgroundCronJobScheduler? _instance;
    public static BackgroundCronJobScheduler Instance => _instance ??= new BackgroundCronJobScheduler();
    public readonly IList<ScheduledTask> ScheduledTasks = new List<ScheduledTask>();

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

        ScheduledTasks.Add(new ScheduledTask(timer));
    }

    public void Clear(Guid scheduleId)
    {
        if (!ScheduledTasks.Any(_ => _.ScheduleId.Equals(scheduleId)))
        {
            throw new InvalidDataException();
        }

        var task = ScheduledTasks.First(_ => _.ScheduleId.Equals(scheduleId));
        task.Timer.Stop();
        task.Timer.Close();
        task.Timer.Dispose();
        ScheduledTasks.Remove(task);
    }

    public void ClearAll()
    {
        foreach (var task in ScheduledTasks)
        {
            task.Timer.Stop();
            task.Timer.Close();
            task.Timer.Dispose();
        }

        ScheduledTasks.Clear();
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

public class ScheduledTask
{
    public Guid ScheduleId { get; }
    public DateTime UtcDateTime { get; }
    public Timer Timer { get; }

    public ScheduledTask(Timer timer)
    {
        ScheduleId = Guid.NewGuid();
        UtcDateTime = DateTime.UtcNow;
        Timer = timer;
    }
}
using FluentAssertions;
using ShortSharp.Utility;
using Xunit;

namespace Test.ShortSharp;

public class BackgroundCronJobSchedulerTest
{
    private BackgroundCronJobScheduler _scheduler;
    public BackgroundCronJobSchedulerTest()
    {
        _scheduler = BackgroundCronJobScheduler.Instance;
    }

    [Fact]
    public void AddScheduler()
    {
        _scheduler.ScheduleNew(
            jobFunction: () => Console.WriteLine("Task executed 1"),
            crownIntervalInMinutes: 1);
        _scheduler.ScheduleNew(
            jobFunction: () => Console.WriteLine("Task executed 2"),
            crownIntervalInMinutes: 1);
        _scheduler.ScheduledTasks.Count.Should().Be(2);
    }
    
    [Fact]
    public void ClearSpecificScheduler()
    {
        var id = _scheduler.ScheduledTasks.First().ScheduleId;
        _scheduler.Clear(id);
        
        _scheduler.ScheduledTasks.Count.Should().Be(1);
    }
    
    [Fact]
    public void ClearScheduler()
    {
        _scheduler.ClearAll();
        
        _scheduler.ScheduledTasks.Count.Should().Be(0);
    }
}
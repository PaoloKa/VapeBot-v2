using System;
using System.Collections.Generic;
using System.Text;

namespace Discordbot.Infrastructure.Services.Scheduler
{
    // Example of usage:
    //
    //This Scheduler will start at 9:44 and call after every 1 Hour
    //        MyScheduler.IntervalInHours(9, 44, 1,
    //        () => {
    //            Console.WriteLine("//here write the code that you want to schedule");

    //
    public static class SchedulerFactory
    {
        public static void IntervalInSeconds(int hour, int sec, double interval, Action task)
        {
            interval = interval / 3600;
            SchedulerService.Instance.ScheduleTask(hour, sec, interval, task);
        }
        public static void IntervalInMinutes(int hour, int min, double interval, Action task)
        {
            interval = interval / 60;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }
        public static void IntervalInHours(int hour, int min, double interval, Action task)
        {
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }
        public static void IntervalInDays(int hour, int min, double interval, Action task)
        {
            interval = interval * 24;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }
    }
}

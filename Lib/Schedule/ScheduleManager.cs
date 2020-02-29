//using Quartz;
//using Quartz.Impl;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Lib
//{
//    public class ScheduleManager
//    {
//        private static StdSchedulerFactory schedulerFactory = new StdSchedulerFactory();
//        private static IScheduler scheduler = schedulerFactory.GetScheduler().Result;

//        static ScheduleManager()
//        {
//            //scheduler.JobFactory = IOCJobFactory;
//            scheduler.Start();
//        }

//        private static string NewToken()
//        {
//            return Guid.NewGuid().ToString("N");
//        }

//        public static async void CreateJob<T>(string cron, DateTime? endTime = null) where T : IJob
//        {
//            var name = NewToken();
//            var group = NewToken();
//            var job = JobBuilder.Create<T>()
//                             .WithIdentity(name, group)
//                             .Build();

//            var cronTrigger = TriggerBuilder.Create()
//                                       .StartAt(DateBuilder.NextGivenSecondDate(DateTime.Now, 1))
//                                       .EndAt(endTime ?? DateTime.Now.AddYears(100))
//                                       .WithIdentity(name + "Trigger", group + "Trigger")
//                                       .WithCronSchedule(cron)
//                                       .Build();

//            await scheduler.ScheduleJob(job, cronTrigger);
//        }

//        public static async void CreateJobRepeat<T>(string cron, int repeatCount, Dictionary<string, object> keyValues = default, DateTimeOffset startAt = default) where T : IJob
//        {
//            var name = NewToken();
//            var group = NewToken();
//            var job = JobBuilder.Create<T>()
//                             .WithIdentity(name, group)
//                             .Build();

//            if (keyValues != default)
//            {
//                foreach (var item in keyValues)
//                {
//                    job.JobDataMap[item.Key] = item.Value;
//                }
//            }

//            if (startAt == null)
//            {
//                startAt = DateBuilder.NextGivenSecondDate(DateTime.Now, 1);
//            }

//            var cronTrigger = TriggerBuilder.Create()
//                                       .StartAt(startAt)
//                                       .WithIdentity(name + "Trigger", group + "Trigger")
//                                       .WithCronSchedule(cron)
//                                       .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForTotalCount(repeatCount))
//                                       .Build();

//            await scheduler.ScheduleJob(job, cronTrigger);
//        }
//    }
//}

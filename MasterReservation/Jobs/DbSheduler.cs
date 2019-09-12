using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace MasterReservation.Jobs
{
    public class DbSheduler
    {

        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<DbService>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                .StartAt(new DateTimeOffset(DateTime.Now.AddDays(1)))                         
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInHours(24)         
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }

    }
}
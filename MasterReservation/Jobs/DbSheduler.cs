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
                //.StartNow()
                //.StartAt(new DateTimeOffset(DateTime.Now.AddMinutes(5)))
                .StartAt(new DateTimeOffset(DateTime.Today.AddDays(1)))
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInHours(24)
                    //.WithIntervalInMinutes(5) 
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }

    }
}
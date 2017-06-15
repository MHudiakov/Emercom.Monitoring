using System;
using DAL.WCF;
using Quartz;
using Quartz.Impl;

namespace BleReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Настройка подключения к серверу
            var context = new WcfDataContext(Settings.Default.ServiceOperation);
            var dataManager = new WcfDataManager(context);
            DalContainer.RegisterWcfDataMager(dataManager);

            // Запускаем задачу считывания пришедших тегов
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            IJobDetail job = JobBuilder.Create<ReaderTask>()
                .WithIdentity("Reader", "ReaderGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ReaderTrigger", "ReaderGroup")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Settings.Default.PollingInterval)
                .RepeatForever())
                .StartAt(DateTime.UtcNow.AddSeconds(0))
                .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}

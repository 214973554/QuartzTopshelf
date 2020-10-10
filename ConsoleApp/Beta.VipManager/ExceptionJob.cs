using Beta.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Beta.VipManager
{
    [DisallowConcurrentExecution]
    public class ExceptionJob : IJob
    {
        private const string ClassFullName = "ConsoleApp.Beta.VipManager.ExceptionJob";
        public static async Task RegisterJob(IScheduler scheduler)
        {
            // 创建Job实例
            IJobDetail job = JobBuilder.Create<ExceptionJob>()
                .WithIdentity("演示作业异常处理", "VipManager")
                .Build();

            // 创建触发器实例
            ITrigger trigger = TriggerBuilder.Create()
                //.StartAt(DateBuilder.DateOf(0, 0, 0))
                //.WithSimpleSchedule(x => { x.WithInterval(TimeSpan.FromDays(1)).RepeatForever(); })
                .StartNow()
                .WithSimpleSchedule(x => { x.WithIntervalInSeconds(5).RepeatForever(); })
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public Task Execute(IJobExecutionContext context)
        {
            LogHelper.Write($"{ClassFullName} 开始执行Job->演示作业异常处理", LogMsgLevel.Info);

            try
            {
                int zero = 0;
                int result = 1 / zero;
            }
            catch (Exception ex)
            {
                LogHelper.Write($"{ClassFullName} Job异常捕获->演示作业异常处理", ex);
            }

            LogHelper.Write($"{ClassFullName} 结束执行Job->演示作业异常处理", LogMsgLevel.Info);

            return Task.CompletedTask;
        }
    }
}

using Beta.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Beta.VipManager
{
    [DisallowConcurrentExecution]
    public class ArticleAdsSubOrgsJob : IJob
    {
        private const string ClassFullName = "ConsoleApp.Beta.VipManager.ArticleAdsSubOrgsJob";
        public static async Task RegisterJob(IScheduler scheduler)
        {
            // 创建Job实例
            IJobDetail job = JobBuilder.Create<ArticleAdsSubOrgsJob>()
                .WithIdentity("文章广告部门可见范围当前部门和所有子部门表【Tb_ArticleAdsSubOrgs】", "VipManager")
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
            LogHelper.Write($"{ClassFullName} 开始执行Job->管理驾驶舱文章广告，部门可见范围同步所选部门的所有子部门入库", LogMsgLevel.Info);

            Thread.Sleep(TimeSpan.FromMinutes(1));

            LogHelper.Write($"{ClassFullName} 结束执行Job->管理驾驶舱文章广告，部门可见范围同步所选部门的所有子部门入库", LogMsgLevel.Info);

            return Task.CompletedTask;
        }
    }
}

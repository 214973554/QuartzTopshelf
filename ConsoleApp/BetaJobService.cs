using Beta.Utils;
using ConsoleApp.Beta.VipManager;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading.Tasks;
using Topshelf;

namespace ConsoleApp
{
    public class BetaJobService
    {
        public async void Start()
        {
            LogHelper.Write("服务开始", LogMsgLevel.Info);

            //1.通过工厂创建作业调度实例
            NameValueCollection properties = new NameValueCollection();

            // 远程输出配置，用于通过web页面对作业调度进行管理
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            //此处端口号必须和web客户端调度端口号保持一致
            properties["quartz.scheduler.exporter.port"] = ConfigurationManager.AppSettings["quartz.scheduler.exporter.port"];
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            var factory = new StdSchedulerFactory(properties);
            IScheduler scheduler = await factory.GetScheduler();

            await PosterMaterialVipManagerExtSubOrgsJob.RegisterJob(scheduler);
            await ArticleAdsSubOrgsJob.RegisterJob(scheduler);
            await ExceptionJob.RegisterJob(scheduler);

            await scheduler.Start();
        }

        public void Stop()
        {
            LogHelper.Write("服务结束", LogMsgLevel.Info);
        }
    }
}
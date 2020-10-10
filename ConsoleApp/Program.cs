using Beta.Utils;
using ConsoleApp.Beta.VipManager;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                // 配置和运行宿主服务
                HostFactory.Run(x =>
                {
                    x.Service<BetaJobService>(s =>
                    {
                        // 指定服务类型。这里设置为 Service
                        s.ConstructUsing(name => new BetaJobService());

                        // 当服务启动后执行什么
                        s.WhenStarted(tc => tc.Start());

                        // 当服务停止后执行什么
                        s.WhenStopped(tc => tc.Stop());
                    });

                    // 服务用本地系统账号来运行
                    x.RunAsLocalSystem();

                    // 服务名称
                    x.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
                    // 服务显示名称
                    x.SetDisplayName(ConfigurationManager.AppSettings["ServiceDisplayName"]);
                    // 服务描述信息
                    x.SetDescription(ConfigurationManager.AppSettings["ServiceDescription"]);
                });

                //直接运行
                //new BetaJobService().Start();
                //Console.ReadKey();
            }
            catch (Exception ex)
            {
                LogHelper.Write("服务启动失败", ex);
            }
        }
    }
}

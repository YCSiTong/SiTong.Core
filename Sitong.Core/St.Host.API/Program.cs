using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace St.Host.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * 玛卡巴卡专用启动命令,请你念出来
             */
            Console.WriteLine("玛卡巴卡 阿卡哇卡 米卡哇卡 姆\r\n" +
                "玛卡巴卡 阿巴呀卡 伊卡阿卡 噢\r\n" +
                "哈姆达姆 阿卡棒 咿呀哟\r\n" +
                "玛卡巴卡 阿卡哇卡\r\n" +
                "米卡马卡 姆\r\n");

            

            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())//注入AutoFac模块
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()// 构建所有基础服务打包返回一个IHost宿主机
                .Run();// 运行
        }
    }
}

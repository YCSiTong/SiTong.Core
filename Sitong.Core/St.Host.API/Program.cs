using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using St.Application.Infrastruct.Identity;
using St.Common.Helper;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

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

            var result = AssemblyHelper.GetAssemblys().SelectMany(op => op.GetTypes())
                .ToArray()
                .Distinct();
            var abstracts = result.Where(op => !op.IsClass && op.IsAbstract && op.IsInterface).ToArray();
            // TODO: 实现生命周期管理，默认全体Scoped。可根据特性自定义注入服务
            foreach (var item in abstracts)
            {
                var singleResult = result.Where(op => /*op.IsClass && !op.IsAbstract && !op.IsInterface &&*/ item.IsAssignableFrom(op)).ToArray();// 注意工厂注入
                if (singleResult.Length > 0 && singleResult.Length < 2)
                {
                    Console.WriteLine($"Interface => { item.Name } \r\n realize => { singleResult[0].Name } \r\n");
                }
            }

            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//注入AutoFac模块
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()// 构建所有基础服务打包返回一个IHost宿主机
                .Run();// 运行
        }
    }
}

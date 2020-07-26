using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace St.Host.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

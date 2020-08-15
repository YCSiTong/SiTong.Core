using Microsoft.Extensions.DependencyInjection;
using St.Extensions;
using System;
using System.Threading.Tasks;

namespace St.ServiceExtensions.Action
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T">服务</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="service"></param>
        /// <param name="func"></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<TResult> CreateService<T, TResult>(this IServiceProvider service, Func<T, Task<TResult>> func)
        {
            if (service.IsNull())
                service = service.CreateScope().ServiceProvider;
            return await func(service.GetService<T>());
        }

        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="action"></param>
        public static void CreateService<T>(this IServiceProvider service, Action<T> action)
        {
            if (service.IsNull())
                service = service.CreateScope().ServiceProvider;
            action(service.GetService<T>());
        }
    }
}

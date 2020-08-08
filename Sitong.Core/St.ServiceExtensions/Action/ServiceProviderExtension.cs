using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace St.ServiceExtensions.Action
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="func"></param>
        /// <returns><see cref="bool"/></returns>
        public static async Task<bool> CreateService<T>(this IServiceProvider service, Func<T, Task<bool>> func)
        {
            var ser = service.CreateScope();
            return await func(ser.ServiceProvider.GetService<T>());
        }

        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="action"></param>
        public static void CreateService<T>(this IServiceProvider service, Action<T> action)
        {
            var ser = service.CreateScope();
            action(ser.ServiceProvider.GetService<T>());
        }

        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="func"></param>
        /// <returns><see cref="bool"/></returns>
        public static async Task<bool> GetService<T>(this IServiceProvider service, Func<T, Task<bool>> func)
        {
            return await func(service.GetService<T>());
        }

        /// <summary>
        /// 创建服务并执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="action"></param>
        public static void GetService<T>(this IServiceProvider service, Action<T> action)
        {
            action(service.GetService<T>());
        }
    }
}

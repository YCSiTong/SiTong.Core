using Microsoft.AspNetCore.Builder;
using St.Application.Infrastruct.Identity;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using St.ServiceExtensions.Action;
using St.DoMain.Model.Identity;
using St.Common.RedisCaChe;
using St.Common.Helper;
using System.Diagnostics;

namespace St.ServiceExtensions.MiddleWare.ApplicationBuilder
{
    /// <summary>
    /// 基础权限信息存储Redis
    /// </summary>
    public static class AuthorizeRedis
    {
        /// <summary>
        /// 添加基础权限信息至Redis中,权限过滤器中可进行使用优化
        /// </summary>
        /// <param name="app"></param>
        public static void AddAuthorizeRedis(this IApplicationBuilder app)
        {
            Stopwatch loadRedisCount = new Stopwatch();
            loadRedisCount.Start();

            // 创建服务区
            var newServiceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            var redisService = newServiceProvider.GetService<IRedisCaChe>();
            Console.Out.WriteLine("==================== 开启记录所有基础权限信息至Redis... ====================\r\n");
            Stopwatch loadRedis = new Stopwatch();

            loadRedis.Start();
            var roleService = newServiceProvider.GetServiceOrCreate<IRoleService, List<Role>>(op =>
            {
                Console.Out.WriteLine("**记录所有角色信息中...");
                var result = op.GetRedis();
                foreach (var item in result)
                {
                    int count = 0;
                    do
                    {
                        var redisResult = redisService.HSetAsync(AllStaticHelper.HRedisRole, item.Id.ToString(), item).Result;
                        if (redisResult) break;
                        ++count;
                        if (count > 0)
                        {
                            if (count == 1)
                                Console.WriteLine();
                            Console.Out.WriteLine($">>> 第{count + 1}次重试记录Id为{{{item.Id}}}的信息录入失败！");
                        }
                    } while (count < 5);
                }
                loadRedis.Stop();
                Console.Out.WriteLine($"**记录所有角色信息完毕！耗时：{loadRedis.Elapsed.TotalSeconds}/s \r\n");
                return result;
            });

            loadRedis.Restart();
            var roleMenuService = newServiceProvider.GetServiceOrCreate<IRoleMenuService, List<RoleMenu>>(op =>
            {
                Console.Out.WriteLine("**记录所有角色菜单信息中...\r\n");
                var result = op.GetRedis();
                foreach (var item in result)
                {
                    int count = 0;
                    do
                    {
                        var redisResult = redisService.HSetAsync(AllStaticHelper.HRedisRoleMenu, item.Id.ToString(), item).Result;
                        if (redisResult) break;
                        ++count;
                        if (count > 0)
                        {
                            if (count == 1)
                                Console.WriteLine();
                            Console.Out.WriteLine($">>> 第{count + 1}次重试记录Id为{{{item.Id}}}的信息录入失败！");
                        }
                    } while (count < 5);
                }
                loadRedis.Stop();
                Console.Out.WriteLine($"**记录所有角色菜单信息完毕！耗时：{loadRedis.Elapsed.TotalSeconds}/s \r\n");
                return result;
            });

            loadRedis.Restart();
            var menuService = newServiceProvider.GetServiceOrCreate<IMenuService, List<Menu>>(op =>
            {
                Console.Out.WriteLine("**记录所有菜单信息中...");
                var result = op.GetRedis();
                foreach (var item in result)
                {
                    int count = 0;
                    do
                    {
                        var redisResult = (redisService.HSetAsync(AllStaticHelper.HRedisMenu, item.Id.ToString(), item).Result);
                        if (redisResult) break;
                        ++count;
                        if (count > 0)
                        {
                            if (count == 1)
                                Console.WriteLine();
                            Console.Out.WriteLine($">>> 第{count + 1}次重试记录Id为{{{item.Id}}}的信息录入失败！");
                        }
                    } while (count < 5);
                }
                loadRedis.Stop();
                Console.Out.WriteLine($"**记录所有菜单信息完毕！耗时：{loadRedis.Elapsed.TotalSeconds}/s \r\n");
                return result;
            });

            loadRedis.Restart();
            var APIService = newServiceProvider.GetServiceOrCreate<IAPIManagementService, List<APIManagement>>(op =>
            {
                Console.Out.WriteLine("**记录所有接口信息中...");
                var result = op.GetRedis();
                foreach (var item in result)
                {
                    int count = 0;
                    do
                    {
                        var redisResult = redisService.HSetAsync(AllStaticHelper.HRedisAPI, item.Id.ToString(), item).Result;
                        if (!redisResult) break;
                        ++count;
                        if (count > 0)
                        {
                            if (count == 1)
                                Console.WriteLine();
                            Console.Out.WriteLine($">>> 第{count + 1}次重试记录Id为{{{item.Id}}}的信息录入失败！");
                        }
                    } while (count < 5);
                }
                loadRedis.Stop();
                Console.Out.WriteLine($"**记录所有接口信息完毕！耗时：{loadRedis.Elapsed.TotalSeconds}/s \r\n");
                return result;
            });

            loadRedis.Restart();
            var roleAPIService = newServiceProvider.GetServiceOrCreate<IRoleAPIManagementService, List<RoleAPIManagement>>(op =>
            {
                Console.Out.WriteLine("**记录所有角色接口权限信息中...");
                var result = op.GetRedis();
                foreach (var item in result)
                {
                    int count = 0;
                    do
                    {
                        var redisResult = redisService.HSetAsync(AllStaticHelper.HRedisRoleAPI, item.Id.ToString(), item).Result;
                        if (redisResult) break;
                        ++count;
                        if (count > 0)
                        {
                            if (count == 1)
                                Console.WriteLine();
                            Console.Out.WriteLine($">>> 第{count + 1}次重试记录Id为{{{item.Id}}}的信息录入失败！");
                        }
                    } while (count < 5);
                }
                loadRedis.Stop();
                Console.Out.WriteLine($"**记录所有角色接口权限信息完毕！耗时：{loadRedis.Elapsed.TotalSeconds}/s \r\n");
                return result;
            });
            loadRedisCount.Stop();

            Console.Out.WriteLine($"====================     本次耗费时长：{loadRedisCount.Elapsed.TotalSeconds}/s     ====================\r\n");
        }
    }
}

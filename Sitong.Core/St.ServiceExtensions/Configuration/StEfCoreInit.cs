using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using St.DoMain.Core.Interfaces;
using St.DoMain.Interfaces;
using St.Extensions;
using System;

namespace St.ServiceExtensions.Configuration
{
    public static class StEfCoreInit
    {
        /// <summary>
        /// 注入DbContext服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddDbContextStartUp<TDbContext>(this IServiceCollection services, Action<DbContextOptions> options)
            where TDbContext : DbContext
        {
            services.NotNull(nameof(IServiceCollection));
            services.NotNull(nameof(Action<DbContextOptions>));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            var model = new DbContextOptions();
            options(model);
            model.ConnectionString.NotEmptyOrNull(nameof(model.ConnectionString));

            services.AddDbContext<TDbContext>(op =>
            {
                switch (model.DataBase)
                {
                    case DataBaseType.SqlServer:
                        op.UseSqlServer(model.ConnectionString);
                        break;
                    case DataBaseType.MySql:
                        throw new Exception("当前未适配MySql形式数据");
                }
            });
        }
    }
    /// <summary>
    /// DbContext配置
    /// </summary>
    public class DbContextOptions
    {
        /// <summary>
        /// 链接数据库字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DataBase { get; set; }
    }
    /// <summary>
    /// 选择数据库
    /// </summary>
    public enum DataBaseType
    {
        SqlServer,
        MySql,
    }
}

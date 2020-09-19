using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using St.DoMain.Core.Repository;
using St.DoMain.Core.UnitOfWork;
using St.DoMain.Repository;
using St.DoMain.UnitOfWork;
using St.EfCore;
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

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>));// 注入工作单元
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));// 注入仓储

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
                        op.UseMySql(model.ConnectionString);
                        break;
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
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer,
        /// <summary>
        /// MySQL
        /// </summary>
        MySql,
    }
}

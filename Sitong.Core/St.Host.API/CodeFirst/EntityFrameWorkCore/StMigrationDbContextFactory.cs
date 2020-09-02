using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using St.Extensions;
using St.ServiceExtensions.Configuration;
using System;

namespace St.Host.API.CodeFirst.EntityFrameWorkCore
{
    public class StMigrationDbContextFactory : IDesignTimeDbContextFactory<StMigrationDbContext>
    {
        public StMigrationDbContext CreateDbContext(string[] args)
        {
            /*
             * 摘要：
             *      当前可自行选择使用开发环境下或正式环境下操作的配置文件进行读取配置,
             *      进行快速迁移数据库
             */

#if DEBUG
            var configuration = new ConfigurationBuilder()
                                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                        .AddJsonFile("appsettings.Development.json")
                                   .Build();
#else
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
#endif



            var dbType = configuration["SqlDbContext:DbType"];
            var thisDbType = dbType.AsTo<DataBaseType>();
            return thisDbType switch
            {
                DataBaseType.SqlServer => new StMigrationDbContext(new DbContextOptionsBuilder<StMigrationDbContext>().UseSqlServer(configuration["SqlDbContext:" + dbType + ":SqlConnection"]).Options),
                DataBaseType.MySql => new StMigrationDbContext(new DbContextOptionsBuilder<StMigrationDbContext>().UseMySql(configuration["SqlDbContext:" + dbType + ":SqlConnection"]).Options),
                _ => throw new ArgumentException("请选择当前支持的数据库类型!")
            };
        }
    }
}

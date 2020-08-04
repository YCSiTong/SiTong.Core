using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using St.Common.Helper;
using St.EfCore;
using St.Extensions;
using St.ServiceExtensions.Configuration;
using St.ServiceExtensions.Configuration.AutoFac;
using St.ServiceExtensions.MiddleWare.Configuration;
using StackExchange.Profiling;

namespace St.Host.API
{
    public class Startup
    {
        private const string _DefaultCorsName = "Sitong";

        private IConfiguration Configuration { get; }//获取appsettings.json
        private IWebHostEnvironment Env { get; } //获取运行地址

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注入AppSettings操作帮助类
            services.AddSingleton(new AppSettings(Env.ContentRootPath));
            #endregion
            #region 注入Redis服务 
            if (Configuration["Redis:Enabled"].ToBool())
            {
                services.AddRedisStartUp();
            }
            #endregion
            #region 注入MemoryCaChe服务
            if (Configuration["MemoryCaChe:Enabled"].ToBool())
            {
                services.AddMemoryCaCheStartUp();
            }
            #endregion
            #region 注入Swagger
            if (Configuration["SwaggerOptions:Enabled"].ToBool())
            {
                services.AddSwaggerStartUp(op =>
                {
                    op.Name = Configuration["SwaggerOptions:Name"];
                    op.Url = Configuration["SwaggerOptions:Url"];
                    op.Version = Configuration["SwaggerOptions:Version"];
                    op.Email = Configuration["SwaggerOptions:Email"];
                    op.Description = Configuration["SwaggerOptions:Description"];
                });
            }
            #endregion
            #region 注入验证授权
            if (Configuration["Authorize:Enabled"].ToBool())
            {
                services.AddAuthorizeStartUp(op =>
                {
                    op.Name = Configuration["Authorize:Name"];
                    op.Aud = Configuration["Authorize:Aud"];
                    op.Minutes = 2;
                    op.SginKey = Configuration["Authorize:SginKey"];
                });
            }
            #endregion
            #region 注入MiniProfiler
            if (Configuration["MiniProfiler:Enabled"].ToBool())
            {
                services.AddMiniProfilerStartUp(op => op = RenderPosition.Left);
            }
            #endregion
            #region 注入AutoMapper
            services.AddAutoMapperStartUp();
            #endregion
            #region 注入Cors
            if (Configuration["Cors:Enabled"].ToBool())
            {
                services.AddCorsStartUp(op =>
                {
                    op.Name = _DefaultCorsName;
                    // 允许全部跨域
                    op.Type = CorsOpenType.All;

                    // 只允许自定义配置的地址跨域
                    //op.Type = CorsOpenType.Custom;
                    //op.Origins = Configuration["Cors:Server"].Split(',');
                });
            }
            #endregion
            #region 注入EfCore中DbContext
            services.AddDbContextStartUp<StDbContext>(op =>
            {
                op.ConnectionString = Configuration["SqlDbContext:SqlServer:SqlConnection"];
                op.DataBase = DataBaseType.SqlServer;
            });
            #endregion

            services.AddControllers();

        }

        /// <summary>
        /// AutoFac工厂
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new StAutoFacModuleRegister());//模块加载.
        }

        /// <summary>
        /// 管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 开启Swagger
            if (Configuration["SwaggerOptions:Enabled"].ToBool())
            {
                app.UseSwagger(op =>
                {
                    op.Name = Configuration["SwaggerOptions:Name"];
                    op.Version = Configuration["SwaggerOptions:Version"];
                    op.IsOpenMiniProfiler = Configuration["MiniProfiler:Enabled"].ToBool();
                });
            }
            #endregion
            #region 是否开启Cors跨域
            if (Configuration["Cors:Enabled"].ToBool())
            {
                app.UseCors(_DefaultCorsName);
            }
            #endregion
            app.UseRouting();
            #region 开启MiniProfiler性能检测
            app.UseMiniProfiler();
            #endregion
            #region Jwt官方验证
            //开启认证
            app.UseAuthentication();
            //授权中间件
            app.UseAuthorization();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}

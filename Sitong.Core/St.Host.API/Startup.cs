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

        private IConfiguration Configuration { get; }//��ȡappsettings.json
        private IWebHostEnvironment Env { get; } //��ȡ���е�ַ

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// ����ע��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region ע��AppSettings����������
            services.AddSingleton(new AppSettings(Env.ContentRootPath));
            #endregion
            #region ע��Redis���� 
            if (Configuration["Redis:Enabled"].ToBool())
            {
                services.AddRedisStartUp();
            }
            #endregion
            #region ע��MemoryCaChe����
            if (Configuration["MemoryCaChe:Enabled"].ToBool())
            {
                services.AddMemoryCaCheStartUp();
            }
            #endregion
            #region ע��Swagger
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
            #region ע����֤��Ȩ
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
            #region ע��MiniProfiler
            if (Configuration["MiniProfiler:Enabled"].ToBool())
            {
                services.AddMiniProfilerStartUp(op => op = RenderPosition.Left);
            }
            #endregion
            #region ע��AutoMapper
            services.AddAutoMapperStartUp();
            #endregion
            #region ע��Cors
            if (Configuration["Cors:Enabled"].ToBool())
            {
                services.AddCorsStartUp(op =>
                {
                    op.Name = _DefaultCorsName;
                    // ����ȫ������
                    op.Type = CorsOpenType.All;

                    // ֻ�����Զ������õĵ�ַ����
                    //op.Type = CorsOpenType.Custom;
                    //op.Origins = Configuration["Cors:Server"].Split(',');
                });
            }
            #endregion
            #region ע��EfCore��DbContext
            services.AddDbContextStartUp<StDbContext>(op =>
            {
                op.ConnectionString = Configuration["SqlDbContext:SqlServer:SqlConnection"];
                op.DataBase = DataBaseType.SqlServer;
            });
            #endregion

            services.AddControllers();

        }

        /// <summary>
        /// AutoFac����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new StAutoFacModuleRegister());//ģ�����.
        }

        /// <summary>
        /// �ܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region ����Swagger
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
            #region �Ƿ���Cors����
            if (Configuration["Cors:Enabled"].ToBool())
            {
                app.UseCors(_DefaultCorsName);
            }
            #endregion
            app.UseRouting();
            #region ����MiniProfiler���ܼ��
            app.UseMiniProfiler();
            #endregion
            #region Jwt�ٷ���֤
            //������֤
            app.UseAuthentication();
            //��Ȩ�м��
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

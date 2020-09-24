using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using St.Common.Helper;
using St.DoMain.Core.Identity;
using St.DoMain.Identity;
using St.EfCore;
using St.Extensions;
using St.ServiceExtensions.Configuration;
using St.ServiceExtensions.Filter;
using St.ServiceExtensions.MiddleWare;
using St.ServiceExtensions.MiddleWare.ApplicationBuilder;
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
            services.AddSingleton(new AppSettings(Env.ContentRootPath, Env.IsDevelopment()));
            #endregion
            #region �Զ���ȫ������ע��
            services.AddDIAllStartUp();
            #endregion
            #region ע��Redis���� 
            if (Configuration["Redis:Enabled"].ToBool())
            {
                services.AddRedisStartUp(Configuration["Redis:RedisConnection"]);
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
                    op.XmlComments = Configuration["SwaggerOptions:XMLComments"].Split(',');
                });
            }
            #endregion
            #region ע����֤��Ȩ
            if (Configuration["Authorize:Enabled"].ToBool())
            {
                services.AddAuthorizeStartUp(op =>
                {
                    op.Issuer = Configuration["Authorize:Issuer"];
                    op.Aud = Configuration["Authorize:Aud"];
                    op.Minutes = 60 * 60;
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
                var dbType = Configuration["SqlDbContext:DbType"];
                if (dbType.IsNotEmptyOrNull())
                {
                    op.ConnectionString = Configuration["SqlDbContext:" + dbType + ":SqlConnection"];
                    op.DataBase = dbType.AsTo<DataBaseType>();
                }
            });
            #endregion
            #region ����Token => ȫ�ֿ�ע��`IdentityInfo`�ӿ� ��ȡ�����Ϣ
            services.AddHttpContextAccessor();
            services.AddScoped<IdentityInfo>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var token = accessor.HttpContext?.Request?.Headers["Authorization"].ToString();
                return new IdentityInfoRealize(JwtHelper.SerializeJwt(token));
            });
            #endregion
            services.AddControllers(op =>
            {
                op.SuppressAsyncSuffixInActionNames = false;// �Ƿ�ȥ��`Async`
                op.Filters.Add<AuthorizeFilter>();
            }).AddNewtonsoftJson(op =>
            {
                op.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

        #region AutoFac��������
        ///// <summary>
        ///// AutoFac����
        ///// </summary>
        ///// <param name="builder"></param>
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new StAutoFacModuleRegister());//ģ�����.
        //} 
        #endregion

        /// <summary>
        /// �ܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region �Զ���¼����Ȩ����Ϣ��Redis��...
            app.AddAuthorizeRedis();
            #endregion
            #region ȫ���쳣����
            app.UseExHandler();
            #endregion

            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}

            #region �Ƿ���Cors����
            if (Configuration["Cors:Enabled"].ToBool())
            {
                app.UseCors(_DefaultCorsName);
            }
            #endregion
            app.UseRouting();
            #region Jwt��Ȩ��֤
            //������֤
            app.UseAuthentication();
            //��Ȩ�м��
            app.UseAuthorization();
            #endregion
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
            #region ����MiniProfiler���ܼ��
            if (Configuration["MiniProfiler:Enabled"].ToBool())
            {
                app.UseMiniProfiler();
            }
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

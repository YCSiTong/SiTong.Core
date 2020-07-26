using Autofac;
using Autofac.Extras.DynamicProxy;
using St.Common.Helper;
using St.Extensions;
using St.ServiceExtensions.Aop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace St.ServiceExtensions.Configuration.AutoFac
{
    public class StAutoFacModuleRegister : Autofac.Module
    {
        /// <summary>
        /// AutoFac模块 批量注入接口以及Aop拦截指定
        /// TODO:当前未迁移完成,需加入Aop模块以及架构层设计完毕后继续搭建,当前为代码测试.禁止使用
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            #region 需要开启的Aop
            List<Type> types = new List<Type>();
            if (AppSettings.GetVal("Aop", "Redis", "Enabled").ObjToBool())
            {
                var type = typeof(RedisCaCheAop);
                builder.RegisterType(type);
                types.Add(type);
            }
            if (AppSettings.GetVal("Aop", "MemoryCaChe", "Enabled").ObjToBool())
            {
                var type = typeof(MemoryCaCheAop);
                builder.RegisterType(type);
                types.Add(type);
            }
            #endregion

            var ServiceDLL = Path.Combine(basePath, "St.Application.dll");
            var AssemblysService = Assembly.LoadFrom(ServiceDLL);


            builder.RegisterAssemblyTypes(AssemblysService) //注册程序集所有类型
                       .AsImplementedInterfaces()//实现所有接口
                       .InstancePerLifetimeScope()//配置组件,使单个ILifetimeScope中的每个依赖组件或解析()调用都获得相同的共享实例.不同生存期范围内的依赖组件将获得不同的实例.
                       .EnableInterfaceInterceptors()//在目标类型上启用类拦截.拦截器将通过类上的拦截属性确定，或者使用InterceptedBy()添加.只有虚拟方法可以通过这种方式被拦截.
                       .InterceptedBy(types.ToArray());//注册所有需要启动的拦截器 
            base.Load(builder);
        }
    }
}

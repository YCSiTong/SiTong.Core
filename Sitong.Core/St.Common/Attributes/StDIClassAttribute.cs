using Microsoft.Extensions.DependencyInjection;
using System;

namespace St.Common.Attributes
{
    /// <summary>
    /// 单个类依赖注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StDIClassAttribute : Attribute
    {
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        /// <summary>
        /// 接口注入时默认为·<see cref="ServiceLifetime.Singleton"/>·
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        public StDIClassAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}

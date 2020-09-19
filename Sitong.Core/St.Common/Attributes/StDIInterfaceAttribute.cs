using Microsoft.Extensions.DependencyInjection;
using System;

namespace St.Common.Attributes
{
    /// <summary>
    /// 接口依赖注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class StDIInterfaceAttribute : Attribute
    {
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        /// <summary>
        /// 接口注入时默认为·<see cref="ServiceLifetime.Scoped"/>·
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        public StDIInterfaceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}

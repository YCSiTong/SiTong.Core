using Microsoft.Extensions.DependencyInjection;
using System;

namespace St.Common.Attributes
{
    /// <summary>
    /// 此接口将进行依赖注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class StDIAttribute : Attribute
    {
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        public StDIAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}

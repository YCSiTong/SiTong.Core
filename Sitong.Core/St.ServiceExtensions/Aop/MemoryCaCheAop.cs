using Castle.DynamicProxy;
using St.Common.Attributes;
using St.Common.MemoryCaChe;
using St.ServiceExtensions.Aop.BaseCaChe;
using System;
using System.Linq;

namespace St.ServiceExtensions.Aop
{
    /// <summary>
    /// 内存缓存Aop
    /// </summary>
    public class MemoryCaCheAop : CaCheHelper
    {
        private readonly IMemoryCaChe _MemoryCaChe;

        public MemoryCaCheAop(IMemoryCaChe memoryCaChe)
        {
            _MemoryCaChe = memoryCaChe;
        }

        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if (method.GetCustomAttributes(true).FirstOrDefault(op => op.GetType() == typeof(StCaCheAttribute)) is StCaCheAttribute stCaChe)
            {
                var key = CaCheKey(invocation);
                var value = _MemoryCaChe.GetVal(key);
                if (value != null)
                {
                    invocation.ReturnValue = value;
                    return;
                }

                invocation.Proceed();

                if (!String.IsNullOrEmpty(key))
                {
                    _MemoryCaChe.SetVal(key, invocation.ReturnValue, TimeSpan.FromMinutes(stCaChe.ExpirationTime));
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}

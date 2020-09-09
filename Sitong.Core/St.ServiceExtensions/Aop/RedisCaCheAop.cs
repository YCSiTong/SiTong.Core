using Castle.DynamicProxy;
using Newtonsoft.Json;
using St.Common.Attributes;
using St.Common.RedisCaChe;
using St.ServiceExtensions.Aop.BaseCaChe;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace St.ServiceExtensions.Aop
{
    public class RedisCaCheAop : CaCheHelper
    {
        private readonly IRedisCaChe _RedisCaChe;

        public RedisCaCheAop(IRedisCaChe redisCaChe)
        {
            _RedisCaChe = redisCaChe;
        }

        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;//获取当前方法

            if (method.GetCustomAttributes(true).FirstOrDefault(op => op.GetType() == typeof(StCaCheAttribute)) is StCaCheAttribute stCaChe)
            {
                var cacheKey = CaCheKey(invocation);
                //注意是 string 类型，方法GetValue
                var cacheValue = _RedisCaChe.GetAsync(cacheKey).Result;
                if (!string.IsNullOrEmpty(cacheValue))
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    var type = invocation.Method.ReturnType;
                    var resultTypes = type.GenericTypeArguments;
                    if (type.FullName == "System.Void")
                    {
                        return;
                    }
                    object response;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        //返回Task<T>
                        if (resultTypes.Any())
                        {
                            var resultType = resultTypes.FirstOrDefault();
                            // 核心1，直接获取 dynamic 类型
                            dynamic temp = JsonConvert.DeserializeObject(cacheValue, resultType);
                            response = Task.FromResult(temp);
                        }
                        else
                            //Task 无返回方法 指定时间内不允许重新运行
                            response = Task.Yield();
                    }
                    else
                    {
                        // 核心2，要进行 ChangeType
                        response = Convert.ChangeType(_RedisCaChe.GetAsync<object>(cacheKey).Result, type);
                    }

                    invocation.ReturnValue = response;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();

                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    object response;

                    //Type type = invocation.ReturnValue?.GetType();
                    var type = invocation.Method.ReturnType;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if (response == null) response = string.Empty;

                    _RedisCaChe.SetAsync(cacheKey, response, TimeSpan.FromMinutes(stCaChe.ExpirationTime)).Wait();
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}

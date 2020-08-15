using Microsoft.AspNetCore.Builder;
using St.ServiceExtensions.MiddleWare.GlobaException;

namespace St.ServiceExtensions.MiddleWare
{
    public static class MiddleWareAll
    {
        /// <summary>
        /// 开启全局异常处理
        /// </summary>
        /// <param name="app"></param>
        public static void UseExHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobaExceptionMiddleWare>();
        }
    }
}

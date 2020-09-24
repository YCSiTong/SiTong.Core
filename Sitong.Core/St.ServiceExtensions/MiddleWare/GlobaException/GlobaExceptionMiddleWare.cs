using Microsoft.AspNetCore.Http;
using St.Exceptions;
using System;
using System.Threading.Tasks;

namespace St.ServiceExtensions.MiddleWare.GlobaException
{
    public class GlobaExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobaExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // TODO:可继续扩展更多自定义异常
            if (ex is BusinessException)// Service内部主动throw
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Service Is Error Please Try again !!!\r\n");
                await context.Response.WriteAsync("ErrorMsg: " + ex.Message);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Service Is Error Please Try again !!!\r\n");
                await context.Response.WriteAsync("ErrorMsg: " + ex.Message);
            }

        }
    }

    internal class StError
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
    }
}

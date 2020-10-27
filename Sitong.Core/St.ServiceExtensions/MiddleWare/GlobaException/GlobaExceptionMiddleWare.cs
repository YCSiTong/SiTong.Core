using Microsoft.AspNetCore.Http;
using St.Exceptions;
using St.Extensions;
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            // TODO:可继续扩展更多自定义异常
            if (ex is BusinessException err)// Service内部主动throw
            {
                var errorJson = (new StError { Msg = err.Message, Code = err.Code }).ToJson();
                await context.Response.WriteAsync(errorJson);
            }
            else
            {
                var errorJson = (new StError { Title = "Service is unavailable !!!", Msg = ex.Message, Code = "999999" }).ToJson();
                await context.Response.WriteAsync(errorJson);
            }

        }
    }

    internal class StError
    {
        /// <summary>
        /// 标题头
        /// </summary>
        public string Title { get; set; } = "Service is error, Please Try again !!!";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
    }
}

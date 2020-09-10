using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using St.Application.Infrastruct.PermissionAuthorize;
using St.DoMain.Identity;
using System.Linq;
using System.Threading.Tasks;
using St.Extensions;
using System;

namespace St.ServiceExtensions.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class AuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IIdentityAuthorize _IdentityAuthorize;
        private readonly IdentityInfo _IdentityInfo;

        public AuthorizeFilter(IIdentityAuthorize identityAuthorize, IdentityInfo identityInfo)
        {
            _IdentityAuthorize = identityAuthorize;
            _IdentityInfo = identityInfo;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_IdentityInfo.Identity.UId.IsNotNull() && _IdentityInfo.Identity.Role.IsNotNull())// 校验是否携带身份信息
            {
                if (!context.ActionDescriptor.EndpointMetadata.Any(op => op is AllowAnonymousAttribute))// 是否需跳过权限验证
                {
                    var apiUrl = context.HttpContext.Request.Path.Value;
                    if (!await _IdentityAuthorize.IsPassAuthorize(apiUrl))// 验证是否拥有接口权限
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult(new { Msg = "当前用户未授权！" });
                    }
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using St.Extensions;
using System;
using System.Text;

namespace St.ServiceExtensions.Configuration
{
    public static class StAuthorizeInit
    {
        public static void AddAuthorizeStartUp(this IServiceCollection services, Action<AuthorizeOptions> options)
        {
            services.NotNull(nameof(IServiceCollection));
            services.NotNull(nameof(Action<AuthorizeOptions>));

            var model = new AuthorizeOptions();
            options(model);

            model.Name.NotEmptyOrNull(nameof(model.Name));
            model.Aud.NotEmptyOrNull(nameof(model.Aud));
            model.SginKey.NotEmptyOrNull(nameof(model.SginKey));

            var sginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.SginKey));//设置tokenKey
            var tokenParameters = new TokenValidationParameters
            {//实例化生成对象
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = sginKey,
                ValidateIssuer = true,
                ValidIssuer = model.Name,
                ValidAudience = model.Aud,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = model.Minutes.HasValue ? TimeSpan.FromMinutes(model.Minutes.Value) : TimeSpan.FromMinutes(1),//偏差值1s
                RequireExpirationTime = true
            };
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(otp =>
             {
                 otp.TokenValidationParameters = tokenParameters;
             });
        }
    }

    /// <summary>
    /// 验证校验权限配置
    /// </summary>
    public class AuthorizeOptions
    {
        /// <summary>
        /// 唯一校验密钥(最少16位字符)
        /// </summary>
        public string SginKey { get; set; }
        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 有效使用人
        /// </summary>
        public string Aud { get; set; }
        /// <summary>
        /// 偏差值 (秒)
        /// </summary>
        public int? Minutes { get; set; }
    }
}

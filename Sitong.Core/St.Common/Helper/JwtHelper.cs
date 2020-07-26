using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace St.Common.Helper
{
    public class JwtHelper
    {/// <summary>
     /// 获取JwtToken
     /// </summary>
     /// <param name="jwtModel">管理员信息</param>
     /// <returns></returns>
        public static string GetJwtToken(JwtModel jwtModel)
        {
            //获取设定发行名
            var iss = AppSettings.GetVal("Authorize", "Name");
            //获取设定发行名
            var aud = AppSettings.GetVal("Authorize", "Aud");
            //获取设定密码
            var pwd = AppSettings.GetVal("Authorize", "SginKey");

            var cliams = new List<Claim>
            {
                //用户id做唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti,jwtModel.UId.ToString()),
                //开始时间
                new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //限制不可早于这个时间
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //过期时间
                //new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
                //过期时间
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddHours(3).ToString()),
                //发行人
                new Claim(JwtRegisteredClaimNames.Iss,iss), 
                //受众
                new Claim(JwtRegisteredClaimNames.Aud,aud)
            };

            //获取Byte密码
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pwd));
            //设定加密格式加密
            var code = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //设定JwtSecurityToken实例
            var jwt = new JwtSecurityToken
                    (
                        issuer: iss,
                        claims: cliams,
                        signingCredentials: code
                    );

            //序列化JwtSecurityToken设定值
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }

    /// <summary>
    /// Jwt管理员信息
    /// </summary>
    public class JwtModel
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Name { get; set; }

    }
}

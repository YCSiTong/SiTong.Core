using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace St.Common.Helper
{
    public class JwtHelper
    {
        /// <summary>
        /// 获取JwtToken
        /// </summary>
        /// <param name="identity">管理员信息</param>
        /// <returns></returns>
        public static string GetJwtToken(IdentityModel identity)
        {
            identity.NotNull(nameof(IdentityModel));
            //获取设定发行名
            var iss = AppSettings.GetVal("Authorize", "Issuer");
            //获取设定受众
            var aud = AppSettings.GetVal("Authorize", "Aud");
            //获取设定密码
            var pwd = AppSettings.GetVal("Authorize", "SginKey");

            var claims = new List<Claim>
            {
                //用户id做唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti,identity.UId.ToString()),
                //开始时间
                new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //限制不可早于这个时间
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //过期时间
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
                //发行人
                new Claim(JwtRegisteredClaimNames.Iss,iss), 
                //受众
                new Claim(JwtRegisteredClaimNames.Aud,aud),
            };

            // 可以将一个用户的多个角色全部赋予
            identity.Role.ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x.ToString()));
            });
            //获取Byte密码
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pwd));
            //设定加密格式加密
            var code = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //设定JwtSecurityToken实例
            var jwt = new JwtSecurityToken
                    (
                        issuer: iss,
                        claims: claims,
                        signingCredentials: code
                    );

            //序列化JwtSecurityToken设定值
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr">ToKen</param>
        /// <returns></returns>
        public static IdentityModel SerializeJwt(string jwtStr)
        {
            if (jwtStr.IsNotEmptyOrNull())
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                if (jwtStr.Contains("Bearer"))
                {
                    jwtStr = jwtStr.Split(' ')[1];
                }
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);

                // 判断是否为多角色
                if (role.ToString().Length > 50)
                {
                    var identity = new IdentityModel
                    {
                        UId = (jwtToken.Id).ToGuid(),
                        Role = role.IsNotEmptyOrNull() ? role.ToEntity<Guid[]>() : null
                    };
                    return identity;
                }
                else
                {
                    var identity = new IdentityModel
                    {
                        UId = (jwtToken.Id).ToGuid(),
                        Role = role.IsNotEmptyOrNull() ? new Guid[] { role.ToGuid() } : null
                    };
                    return identity;
                }


            }
            return new IdentityModel();
        }
    }

    /// <summary>
    /// Jwt管理员信息
    /// </summary>
    public class IdentityModel
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public Guid UId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<Guid> Role { get; set; }

    }
}

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
        /// <param name="jwtModel">管理员信息</param>
        /// <returns></returns>
        public static string GetJwtToken(JwtModel jwtModel)
        {
            //获取设定发行名
            var iss = AppSettings.GetVal("Authorize", "Name");
            //获取设定受众
            var aud = AppSettings.GetVal("Authorize", "Aud");
            //获取设定密码
            var pwd = AppSettings.GetVal("Authorize", "SginKey");

            var claims = new List<Claim>
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
                new Claim(JwtRegisteredClaimNames.Aud,aud),
                //new Claim("123","123")
            };

            // 可以将一个用户的多个角色全部赋予
            jwtModel.Role.ForEach(x =>
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
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JwtModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);

            var tm = new JwtModel
            {
                UId = (jwtToken.Id).ToGuid(),
                Role = role.IsNotEmptyOrNull() ? role.ToEntity<IEnumerable<Guid>>() : null
            };
            return tm;


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
        public Guid UId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<Guid> Role { get; set; }

    }
}

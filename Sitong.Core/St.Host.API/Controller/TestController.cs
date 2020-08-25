using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Common.Helper;
using St.DoMain.Identity;
using St.Exceptions;
using St.Extensions;

namespace St.Host.API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IdentityInfo _identityInfo;
        public TestController(IdentityInfo identityinfo)
        {
            _identityInfo = identityinfo;
        }

        /// <summary>
        /// 测试Jwt生成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string CreateTokenTest()
        {
            IdentityModel jwt = new IdentityModel { UId = Guid.NewGuid()/*, Role = new List<Guid> { "5f692903-f473-5062-7a79-adc673f5a287".ToGuid(), Guid.NewGuid(), Guid.NewGuid() }*/ };
            return JwtHelper.GetJwtToken(jwt);

        }

        /// <summary>
        /// 测试Jwt生成以及破解
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IdentityModel ParseTokenTest()
        {
            IdentityModel jwt = new IdentityModel { UId = Guid.NewGuid()/*, Role = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }*/ };
            var token = JwtHelper.GetJwtToken(jwt);
            return JwtHelper.SerializeJwt(token);
        }

        /// <summary>
        /// 获取解析的身份信息
        /// </summary>
        [HttpGet, Authorize]
        public IdentityModel GetIdentityInfo()
            => _identityInfo.Identity;

        /// <summary>
        /// Throw 异常测试
        /// </summary>
        [HttpGet]
        public void ThrowExTest()
        {
            throw new BusinessException("主动抛出的异常信息");
        }
    }
}

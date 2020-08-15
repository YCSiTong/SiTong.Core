using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Common.Helper;
using St.Exceptions;

namespace St.Host.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试Jwt生成以及破解
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JwtModel Test()
        {
            JwtModel jwt = new JwtModel { UId = Guid.NewGuid(), Role = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() } };
            var token = JwtHelper.GetJwtToken(jwt);
            return JwtHelper.SerializeJwt(token);
        }

        /// <summary>
        /// Throw 异常测试
        /// </summary>
        [HttpGet]
        public void ThrowEx()
        {
            throw new BusinessException("主动抛出的异常信息");
        }
    }
}

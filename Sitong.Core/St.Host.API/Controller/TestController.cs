﻿using Microsoft.AspNetCore.Mvc;
using St.Common.GuidMethods;
using St.Common.Helper;
using St.DoMain.Identity;
using St.Exceptions;
using System;
using System.Collections.Generic;

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
            IdentityModel jwt = new IdentityModel { UId = Guid.NewGuid(), Role = new List<Guid> { GuidAll.NewGuid(), GuidAll.NewGuid(), GuidAll.NewGuid() } };
            return JwtHelper.GetJwtToken(jwt);

        }

        /// <summary>
        /// 测试Jwt生成以及破解
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IdentityModel ParseTokenTest()
        {
            IdentityModel jwt = new IdentityModel { UId = Guid.NewGuid(), Role = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() } };
            var token = JwtHelper.GetJwtToken(jwt);
            return JwtHelper.SerializeJwt(token);
        }

        /// <summary>
        /// 获取解析的身份信息
        /// </summary>
        [HttpGet]
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

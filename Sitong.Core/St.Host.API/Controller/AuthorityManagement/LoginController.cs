using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.Common.Helper;

namespace St.Host.API.Controller.AuthorityManagement
{
    /// <summary>
    /// 登录相关控制
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// 登录后台账户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> LoginAdmin(string account, string pwd)
        {
            var userDto = await _userService.LoginAsync(account, pwd);
            return JwtHelper.GetJwtToken(new IdentityModel { UId = userDto.Id, Role = new Guid[] { Guid.NewGuid() } });
        }
    }
}

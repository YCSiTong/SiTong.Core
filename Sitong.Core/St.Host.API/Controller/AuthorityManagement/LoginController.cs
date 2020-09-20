using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.Common.Helper;
using St.DoMain.Identity;
using St.Exceptions;

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
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleMenuService _roleMenuService;
        private readonly IMenuService _menuService;
        private readonly IdentityInfo _identityInfo;

        public LoginController(
            IUserService userService
            , IUserRoleService userRoleService
            , IRoleMenuService roleMenuService
            , IMenuService menuService
            , IdentityInfo identityInfo
            )
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleMenuService = roleMenuService;
            _menuService = menuService;
            _identityInfo = identityInfo;
        }


        /// <summary>
        /// 登录后台账户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultDto<string>> LoginAdminAsync(string account, string pwd)
        {
            var userDto = await _userService.LoginAsync(account, pwd);
            var userRoles = (await _userRoleService.GetListAsync(userDto.Id)).Select(x => x.RoleId);
            return new ResultDto<string> { Result = JwtHelper.GetJwtToken(new IdentityModel { UId = userDto.Id, Role = userRoles }) };
        }

        /// <summary>
        /// 获取后台菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultDto<string>> GetMenuListAsync()
        {
            var roles = _identityInfo.Identity.Role;
            var roleMenuDtos = await _roleMenuService.GetMenuListAsync(roles);
            var menuDtos = await _menuService.GetAdminMenuListAsync(roleMenuDtos);
            throw new BusinessException("请完善通用递归实现菜单排序并论级！");
        }
    }
}

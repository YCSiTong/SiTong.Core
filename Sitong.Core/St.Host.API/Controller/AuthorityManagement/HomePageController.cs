using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Identity.Menu.Regiter;
using St.AutoMapper.Identity.User.Register;

namespace St.Host.API.Controller.AuthorityManagement
{
    /// <summary>
    /// 首页相关
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class HomePageController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IUserService _userService;

        public HomePageController(IMenuService menuService, IUserService userService)
        {
            _menuService = menuService;
            _userService = userService;
        }

        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="dto">所需新增信息</param>
        /// <returns>是否成功</returns>
        [HttpPost]
        public async Task<bool> CreateMenu([FromQuery] MenuCreateDto dto)
            => await _menuService.InsertAsync(dto);

        /// <summary>
        /// 新增管理员信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAdminUser([FromQuery] UserCreateDto dto)
            => await _userService.InsertAsync(dto);
    }
}

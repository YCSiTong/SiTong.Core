using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Identity.User;
using St.AutoMapper.Identity.User.Register;

namespace St.Host.API.Controller.AuthorityManagement
{
    /// <summary>
    /// 管理员账户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdministratorController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 分页获取管理员列表
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResultDto<UserViewDto>> GetListAsync(ParameterUserDto dto)
           => await _userService.GetListAsync(dto);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultDto<bool>> DeleteAsync(Guid Id)
            => new ResultDto<bool> { Result = await _userService.DeleteAsync(Id) };

        /// <summary>
        /// 修改管理员基本信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultDto<bool>> UpdateAsync(Guid Id, UserUpdateDto dto)
            => new ResultDto<bool> { Result = await _userService.UpdateAsync(Id, dto) };

        /// <summary>
        /// 冻结或解冻管理员账户
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultDto<bool>> OpenOrCloseFreezeAsync(Guid Id)
            => new ResultDto<bool> { Result = await _userService.OpenOrCloseFreezeAsync(Id) };

        /// <summary>
        /// 新增管理员基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultDto<bool>> CreateAsync(UserCreateDto dto)
            => new ResultDto<bool> { Result = await _userService.InsertAsync(dto) };
    }
}

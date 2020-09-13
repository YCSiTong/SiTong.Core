using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Identity.UserRole;
using St.AutoMapper.Identity.UserRole.Register;

namespace St.Host.API.Controller.AuthorityManagement
{
    /// <summary>
    /// 管理员权限管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministratorRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public AdministratorRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// 根据管理员编号获取所有所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserRoleViewDto>> GetListAsync(Guid userId)
            => await _userRoleService.GetListAsync(userId);

        /// <summary>
        /// 新增管理员所属角色
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<ResultDto<bool>> CreateAsync(UserRoleCreateDto dto)
            => new ResultDto<bool> { Result = await _userRoleService.InsertAsync(dto) };
        /// <summary>
        /// 删除指定管理员所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        public async Task<ResultDto<bool>> DeleteAsync(Guid userId)
            => new ResultDto<bool> { Result = await _userRoleService.DeleteAsync(userId) };
    }
}

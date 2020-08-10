using St.AutoMapper.Common;
using St.AutoMapper.Identity.RoleMenu;
using System;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IRoleMenuService
    {
        /// <summary>
        /// 分页获取角色菜单信息
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        Task<PageResultDto<RoleMenuViewDto>> GetListAsync(ParameterRoleMenuDto dto);
        /// <summary>
        /// 新增角色所拥有菜单权限
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="menuIds">权限组主键</param>
        /// <returns></returns>
        Task<bool> SetRoleMenuGroup(Guid Id, Guid[] menuIds);

    }
}

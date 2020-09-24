using St.Common.Attributes;
using St.DoMain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    [StDIInterface]
    public interface IRoleMenuService
    {
        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        List<RoleMenu> GetRedis();
        /// <summary>
        /// 获取指定权限的菜单编号
        /// </summary>
        /// <param name="guids">角色编号</param>
        /// <returns></returns>
        Task<IEnumerable<Guid>> GetMenuListAsync(IEnumerable<Guid> guids);
        /// <summary>
        /// 新增角色所拥有菜单权限
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="menuIds">权限组主键</param>
        /// <returns></returns>
        Task<bool> SetRoleMenuGroup(Guid Id, Guid[] menuIds);

    }
}

using St.AutoMapper.Identity.UserRole;
using St.AutoMapper.Identity.UserRole.Register;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IUserRoleService
    {
        /// <summary>
        /// 根据管理员编号获取所有所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        Task<IEnumerable<UserRoleViewDto>> GetListAsync(Guid userId);
        /// <summary>
        /// 新增管理员所属角色
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        Task<bool> InsertAsync(UserRoleCreateDto dto);
        /// <summary>
        /// 删除指定管理员所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid userId);
    }
}

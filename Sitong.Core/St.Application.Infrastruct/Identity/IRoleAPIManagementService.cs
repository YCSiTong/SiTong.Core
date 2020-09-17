using St.AutoMapper.Common;
using St.AutoMapper.Identity.RoleAPIManagement;
using St.AutoMapper.Identity.RoleAPIManagement.Regiter;
using St.Common.Attributes;
using St.DoMain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    [StDI]
    public interface IRoleAPIManagementService
    {
        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        List<RoleAPIManagement> GetRedis();
        /// <summary>
        /// 根据条件分页获取角色接口权限信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        Task<PageResultDto<RoleAPIManagementViewDto>> GetListAsync(ParameterRoleAPIManagementDto dto);
        /// <summary>
        /// 新增角色接口权限信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        Task<bool> InsertAsync(RoleAPIManagementCreateDto dto);
        /// <summary>
        /// 修改角色接口权限信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, RoleAPIManagementUpdateDto dto);
        /// <summary>
        /// 删除角色接口权限信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id);
    }
}

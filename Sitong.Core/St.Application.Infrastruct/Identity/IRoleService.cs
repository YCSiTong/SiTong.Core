using St.AutoMapper.Common;
using St.AutoMapper.Identity.Role;
using St.AutoMapper.Identity.Role.Regiter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取分页查询角色数据
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        Task<PageResultDto<RoleViewDto>> GetListAsync(ParameterRoleDto dto);
        /// <summary>
        /// 根据主键获取指定的角色信息(支持批量)
        /// </summary>
        /// <param name="Ids">主键编号</param>
        /// <returns></returns>
        Task<IEnumerable<RoleViewDto>> GetByIds(params Guid[] Ids);
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="dto">需要新增的数据</param>
        /// <returns></returns>
        Task<bool> InsertAsync(RoleCreateDto dto);
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id);
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">需要修改的信息</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, RoleUpdateDto dto);
    }
}

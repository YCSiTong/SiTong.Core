using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Identity.RoleAPIManagement;
using St.AutoMapper.Identity.RoleAPIManagement.Regiter;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Exceptions;
using System;
using System.Threading.Tasks;

namespace St.Application.Identity
{
    public class RoleAPIManagementService : IRoleAPIManagementService
    {
        private readonly IRepository<RoleAPIManagement, Guid> _roleAPIManagementRepository;

        public RoleAPIManagementService(IRepository<RoleAPIManagement, Guid> roleAPIManagementRepository)
        {
            _roleAPIManagementRepository = roleAPIManagementRepository;
        }

        /// <summary>
        /// 根据条件分页获取接口信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<RoleAPIManagementViewDto>> GetListAsync(ParameterRoleAPIManagementDto dto)
        {
            throw new BusinessException("");
        }
        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(RoleAPIManagementCreateDto dto)
        {
            throw new BusinessException("");
        }
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, RoleAPIManagementUpdateDto dto)
        {
            throw new BusinessException("");
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            throw new BusinessException("");
        }
    }
}

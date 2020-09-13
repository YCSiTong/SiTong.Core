using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.RoleAPIManagement;
using St.AutoMapper.Identity.RoleAPIManagement.Regiter;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Exceptions;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        public List<RoleAPIManagement> GetRedis()
            => _roleAPIManagementRepository.AsNoTracking().ToList();

        /// <summary>
        /// 根据条件分页获取角色接口权限信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<RoleAPIManagementViewDto>> GetListAsync(ParameterRoleAPIManagementDto dto)
        {
            dto.NotNull(nameof(ParameterRoleAPIManagementDto));
            var roleAPIResult = await _roleAPIManagementRepository.GetListAsync(x => x.CreatedTime, null, dto.SkipCount, dto.MaxResultCount);
            var roleAPIDtos = roleAPIResult.Item1.ToMap<RoleAPIManagementViewDto>();
            var roleAPICount = roleAPIResult.Item2;
            return new PageResultDto<RoleAPIManagementViewDto> { TotalCount = roleAPICount, Result = roleAPIDtos };
        }
        /// <summary>
        /// 新增角色接口权限
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(RoleAPIManagementCreateDto dto)
        {
            dto.NotNull(nameof(RoleAPIManagementCreateDto));
            if (await _roleAPIManagementRepository.IsExistAsync(op => op.RoleId == dto.RoleId && op.APIId == dto.APIId))
            {
                var roleAPIModel = dto.ToMap<RoleAPIManagement>();
                return await _roleAPIManagementRepository.InsertAsync(roleAPIModel);
            }
            throw new BusinessException("当前角色已拥有该接口权限,不可重复添加!!!");
        }
        /// <summary>
        /// 修改角色接口权限
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, RoleAPIManagementUpdateDto dto)
        {
            Id.NotEmpty(nameof(Id));
            dto.NotNull(nameof(RoleAPIManagementUpdateDto));
            /*
             * TODO：
             *      是否需要验证角色/接口存在！
             */
            var roleAPIModel = await _roleAPIManagementRepository.GetByIdAsync(Id);
            if (roleAPIModel.IsNull())
            {
                if (await _roleAPIManagementRepository.IsExistAsync(op => op.RoleId == dto.RoleId && op.APIId == dto.APIId))
                {
                    var roleAPIResult = dto.ToMap(roleAPIModel);
                    return await _roleAPIManagementRepository.UpdateAsync(roleAPIResult);
                }
                else
                    throw new BusinessException("当前角色接口权限已存在!!!");
            }
            throw new BusinessException("当前需修改的角色接口权限不存在!!!");
        }
        /// <summary>
        /// 删除角色接口权限
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            return await _roleAPIManagementRepository.DeleteAsync(Id);
        }
    }
}

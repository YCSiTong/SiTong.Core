using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.Role;
using St.AutoMapper.Identity.Role.Regiter;
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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role, Guid> _roleRepository;

        public RoleService(IRepository<Role, Guid> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRedis()
          => _roleRepository.AsNoTracking().ToList();

        /// <summary>
        /// 获取分页查询角色数据
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<RoleViewDto>> GetListAsync(ParameterRoleDto dto)
        {
            dto.NotNull(nameof(ParameterRoleDto));
            var roleResult = await _roleRepository.GetListAsync(x => x.CreatedTime, null, dto.SkipCount, dto.MaxResultCount);
            var roleDtos = roleResult.Item1.ToMap<RoleViewDto>();
            var totalCoune = roleResult.Item2;
            return new PageResultDto<RoleViewDto> { TotalCount = totalCoune, Result = roleDtos };
        }
        /// <summary>
        /// 根据主键获取指定的角色信息(支持批量)
        /// </summary>
        /// <param name="Ids">主键编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleViewDto>> GetByIds(params Guid[] Ids)
        {
            Ids.NotNull(nameof(Ids));
            return (await _roleRepository.AsNoTracking().Where(op => Ids.Contains(op.Id)).ToListAsync()).ToMap<RoleViewDto>();
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="dto">需要新增的数据</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(RoleCreateDto dto)
        {
            dto.NotNull(nameof(RoleCreateDto));
            if (!await _roleRepository.IsExistAsync(op => op.Name == dto.Name))
            {
                var roleModel = dto.ToMap<Role>();
                return await _roleRepository.InsertAsync(roleModel);
            }
            throw new BusinessException("当前角色名称已存在!!!");
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            return await _roleRepository.DeleteAsync(Id);
        }
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">需要修改的信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, RoleUpdateDto dto)
        {
            Id.NotEmpty(nameof(Id));
            dto.NotNull(nameof(RoleUpdateDto));
            var roleModel = await _roleRepository.GetByIdAsync(Id);
            if (roleModel.IsNotNull())
            {
                var roleResult = dto.ToMap(roleModel);
                return await _roleRepository.UpdateAsync(roleResult);
            }
            throw new BusinessException("当前需修改角色异常,请刷新重试!!!");
        }
    }
}

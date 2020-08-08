using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.UserRole;
using St.AutoMapper.Identity.UserRole.Register;
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
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole, Guid> _userRoleRepository;
        public UserRoleService(IRepository<UserRole, Guid> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 根据管理员编号获取所有所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserRoleViewDto>> GetListAsync(Guid userId)
           => (await _userRoleRepository.AsNoTracking().Where(x => x.UserId == userId).ToListAsync()).ToMap<UserRoleViewDto>();

        /// <summary>
        /// 新增管理员所属角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(UserRoleCreateDto dto)
        {
            var userRoleModel = dto.ToMap<UserRole>();
            if (userRoleModel.IsNotNull())
                return await _userRoleRepository.InsertAsync(userRoleModel);
            throw new BusinessException("请检查传入信息是否正确!!!");
        }
        /// <summary>
        /// 删除指定管理员所属角色
        /// </summary>
        /// <param name="userId">管理员编号</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid userId)
            => await _userRoleRepository.DeleteAsync(userId);
    }
}

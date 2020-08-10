﻿using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.RoleMenu;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace St.Application.Identity
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly IRepository<RoleMenu, Guid> _roleMenuRepository;
        public RoleMenuService(IRepository<RoleMenu, Guid> roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
        }


        /// <summary>
        /// 分页获取角色菜单信息
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<RoleMenuViewDto>> GetListAsync(ParameterRoleMenuDto dto)
        {
            dto.NotNull(nameof(ParameterRoleMenuDto));
            var roleMenuDtos = (await _roleMenuRepository.AsNoTracking().Page(dto.SkipCount, dto.MaxResultCount).ToListAsync()).ToMap<RoleMenuViewDto>();
            var totalCount = await _roleMenuRepository.AsNoTracking().CountAsync();
            return new PageResultDto<RoleMenuViewDto> { TotalCount = totalCount, Result = roleMenuDtos };
        }

        /// <summary>
        /// 新增角色所拥有菜单权限
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="menuIds">权限组主键</param>
        /// <returns></returns>
        public async Task<bool> SetRoleMenuGroup(Guid Id, Guid[] menuIds)
        {
            Id.NotEmpty(nameof(Id));
            menuIds.NotNull(nameof(menuIds));
            return await _roleMenuRepository.UnitOfWork.UseTransactionAsync<bool>(async () =>
             {
                 bool result = false;// 必须所有返回为True
                 var roleMenuModels = await _roleMenuRepository.AsNoTracking().Where(x => x.RoleId == Id).ToListAsync();
                 if (roleMenuModels.Count > 0)
                 {
                     // 寻找当前角色所包含权限组中与需修改的最新权限组不匹配数据,进行删除
                     var removeModel = roleMenuModels.Where(op => !menuIds.Contains(op.MenuId)).ToList();
                     if (removeModel.Count > 0)
                         result = await _roleMenuRepository.DeleteAsync(removeModel);

                     // 需要找出不存在权限组的并进行新增DB
                     List<RoleMenu> roleMenuList = new List<RoleMenu>();
                     foreach (var item in menuIds)
                     {
                         var IsExistRole = roleMenuModels.Where(x => x.MenuId == item).FirstOrDefault();
                         if (IsExistRole.IsNull())
                             roleMenuList.Add(new RoleMenu { RoleId = Id, MenuId = item });
                     }
                     result = await _roleMenuRepository.InsertAsync(roleMenuList);
                 }
                 else
                 {
                     // 创建集合把所有的权限组新增至DB中
                     List<RoleMenu> roleMenuList = new List<RoleMenu>();
                     foreach (var item in menuIds)
                     {
                         roleMenuList.Add(new RoleMenu { RoleId = Id, MenuId = item });
                     }
                     if (roleMenuList.Count > 0)
                         result = await _roleMenuRepository.InsertAsync(roleMenuList);
                 }
                 return result;
             });

        }
    }
}

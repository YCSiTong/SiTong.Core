using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.Menu;
using St.AutoMapper.Identity.Menu.Regiter;
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
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu, Guid> _menuRepository;
        public MenuService(IRepository<Menu, Guid> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetRedis()
            => _menuRepository.AsNoTracking().ToList();
        /// <summary>
        /// 分页所有菜单列表
        /// </summary>
        /// <param name="parDto">请求参数</param>
        /// <returns></returns>
        public async Task<PageResultDto<MenuViewDto>> GetListAsync(ParameterMenuDto dto)
        {
            dto.NotNull(nameof(ParameterMenuDto));
            var menuResult = await _menuRepository.GetListAsync(x => x.CreatedTime, null, dto.SkipCount, dto.MaxResultCount);
            var menuDtos = menuResult.Item1.ToMap<MenuViewDto>();
            var totalCount = menuResult.Item2;
            return new PageResultDto<MenuViewDto> { TotalCount = totalCount, Result = menuDtos };
        }

        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(MenuCreateDto dto)
        {
            dto.NotNull(nameof(MenuCreateDto));
            if (!await _menuRepository.IsExistAsync(op => op.Name == dto.Name || op.Url == dto.Url))
            {
                var menuModel = dto.ToMap<Menu>();
                return await _menuRepository.InsertAsync(menuModel);
            }
            throw new BusinessException("菜单名称或地址已存在相同数据!!!");
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, MenuUpdateDto dto)
        {
            Id.NotEmpty(nameof(Id));
            dto.NotNull(nameof(MenuUpdateDto));
            var menuModel = await _menuRepository.GetByIdAsync(Id);
            if (menuModel.IsNotNull())
            {
                var menuResult = dto.ToMap(menuModel);
                return await _menuRepository.UpdateAsync(menuResult);
            }
            throw new BusinessException("当前需修改菜单信息异常,请刷新重试!!!");
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            return await _menuRepository.DeleteAsync(Id);
        }
    }
}

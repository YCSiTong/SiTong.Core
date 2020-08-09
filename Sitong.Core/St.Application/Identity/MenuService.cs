using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.Menu;
using St.AutoMapper.Identity.Menu.Regiter;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Extensions;
using System;
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
        /// 分页所有菜单列表
        /// </summary>
        /// <param name="parDto">请求参数</param>
        /// <returns></returns>
        public async Task<PageResultDto<MenuViewDto>> GetListAsync(ParameterMenuDto parDto)
        {
            parDto.NotNull(nameof(ParameterMenuDto));
            var menuDtos = (await _menuRepository.AsNoTracking().Page(parDto.SkipCount, parDto.MaxResultCount).ToListAsync()).ToMap<MenuViewDto>();
            var totalCount = await _menuRepository.AsNoTracking().CountAsync();
            return new PageResultDto<MenuViewDto> { TotalCount = totalCount, Result = menuDtos };
        }

        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public Task<bool> InsertAsync(MenuCreateDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(Guid Id, MenuUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

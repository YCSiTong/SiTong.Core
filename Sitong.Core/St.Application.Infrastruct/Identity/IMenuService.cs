using St.AutoMapper.Common;
using St.AutoMapper.Identity.Menu;
using St.AutoMapper.Identity.Menu.Regiter;
using St.DoMain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IMenuService
    {
        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        List<Menu> GetRedis();
        /// <summary>
        /// 分页所有菜单列表
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        Task<PageResultDto<MenuViewDto>> GetListAsync(ParameterMenuDto dto);
        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        Task<bool> InsertAsync(MenuCreateDto dto);
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, MenuUpdateDto dto);
        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id);
    }
}

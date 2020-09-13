using St.AutoMapper.Common;
using St.AutoMapper.Identity.APIManagement;
using St.AutoMapper.Identity.APIManagement.Regiter;
using St.DoMain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IAPIManagementService
    {
        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        List<APIManagement> GetRedis();
        /// <summary>
        /// 根据条件分页获取接口信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        Task<PageResultDto<APIManagementViewDto>> GetListAsync(ParameterAPIManagementDto dto);
        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        Task<bool> InsertAsync(APIManagementCreateDto dto);
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, APIManagementUpdateDto dto);
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id);
        /// <summary>
        /// 开启或关闭接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        Task<bool> OpenOrCloseAPIAsync(Guid Id);
    }
}

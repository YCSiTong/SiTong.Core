using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Identity.APIManagement;
using St.AutoMapper.Identity.APIManagement.Regiter;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Exceptions;
using System;
using System.Threading.Tasks;

namespace St.Application.Identity
{
    public class APIManagementService : IAPIManagementService
    {
        private readonly IRepository<APIManagement, Guid> _apiManagementRepository;

        public APIManagementService(IRepository<APIManagement, Guid> apiManagementRepository)
        {
            _apiManagementRepository = apiManagementRepository;
        }

        /// <summary>
        /// 根据条件分页获取接口信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<APIManagementViewDto>> GetListAsync(ParameterAPIManagementDto dto)
        {
            throw new BusinessException("");
        }
        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(APIManagementCreateDto dto)
        {
            throw new BusinessException("");
        }
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, APIManagementUpdateDto dto)
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
        /// <summary>
        /// 开启或关闭接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public async Task<bool> OpenOrCloseAPI(Guid Id)
        {
            throw new BusinessException("");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.APIManagement;
using St.AutoMapper.Identity.APIManagement.Regiter;
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
    public class APIManagementService : IAPIManagementService
    {
        private readonly IRepository<APIManagement, Guid> _apiManagementRepository;

        public APIManagementService(IRepository<APIManagement, Guid> apiManagementRepository)
        {
            _apiManagementRepository = apiManagementRepository;
        }

        /// <summary>
        /// 获取所有数据存储在Redis中
        /// </summary>
        /// <returns></returns>
        public List<APIManagement> GetRedis()
            => _apiManagementRepository.AsNoTracking().ToList();
        /// <summary>
        /// 根据条件分页获取接口信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        public async Task<PageResultDto<APIManagementViewDto>> GetListAsync(ParameterAPIManagementDto dto)
        {
            dto.NotNull(nameof(ParameterAPIManagementDto));
            var APIDtos = (await _apiManagementRepository.AsNoTracking().Skip(dto.SkipCount).Take(dto.MaxResultCount).ToListAsync()).ToMap<APIManagementViewDto>();
            var APICount = await _apiManagementRepository.AsNoTracking().CountAsync();
            return new PageResultDto<APIManagementViewDto> { TotalCount = APICount, Result = APIDtos };
        }
        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(APIManagementCreateDto dto)
        {
            dto.NotNull(nameof(APIManagementCreateDto));
            var isExist = await _apiManagementRepository.AsNoTracking().Where(op => op.ApiUrl == dto.ApiUrl).CountAsync();
            if (isExist.IsMinus())
            {
                var APIModel = dto.ToMap<APIManagement>();
                APIModel.IsEnabled = true;// 开启接口
                return await _apiManagementRepository.InsertAsync(APIModel);
            }
            throw new BusinessException("新增的接口信息所包含地址已存在!!!");
        }
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, APIManagementUpdateDto dto)
        {
            dto.NotNull(nameof(APIManagementCreateDto));
            var APIModel = await _apiManagementRepository.AsNoTracking().Where(op => op.Id == Id).FirstOrDefaultAsync();
            if (APIModel.IsNotNull())
            {
                var APIResult = dto.ToMap(APIModel);
                return await _apiManagementRepository.UpdateAsync(APIResult);
            }
            throw new BusinessException("当前需修改的接口信息不存在!!!");
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            var APIModel = await _apiManagementRepository.AsNoTracking().Where(op => op.Id == Id).FirstOrDefaultAsync();
            if (APIModel.IsNotNull())
            {
                return await _apiManagementRepository.DeleteAsync(APIModel);
            }
            throw new BusinessException("当前需删除的接口信息不存在!!!");
        }
        /// <summary>
        /// 开启或关闭接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public async Task<bool> OpenOrCloseAPI(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            var APIModel = await _apiManagementRepository.AsTracking().Where(op => op.Id == Id).FirstOrDefaultAsync();
            if (APIModel.IsNotNull())
            {
                if (APIModel.IsEnabled)
                    APIModel.IsEnabled = false;
                else
                    APIModel.IsEnabled = true;
                return await _apiManagementRepository.SaveAsync();
            }
            throw new BusinessException("当前需开/关的接口信息不存在!!!");
        }
    }
}

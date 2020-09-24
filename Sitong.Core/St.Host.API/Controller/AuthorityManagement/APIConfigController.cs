using Microsoft.AspNetCore.Mvc;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Identity.APIManagement;
using St.AutoMapper.Identity.APIManagement.Regiter;
using System;
using System.Threading.Tasks;

namespace St.Host.API.Controller.AuthorityManagement
{
    /// <summary>
    /// API信息以及权限信息管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APIConfigController : ControllerBase
    {
        private readonly IAPIManagementService _apiManagementService;
        private readonly IRoleAPIManagementService _roleAPIManagementService;

        public APIConfigController(IAPIManagementService apiManagementService,
                                   IRoleAPIManagementService roleAPIManagementService
            )
        {
            _apiManagementService = apiManagementService;
            _roleAPIManagementService = roleAPIManagementService;
        }


        /// <summary>
        /// 根据条件分页获取接口信息
        /// </summary>
        /// <param name="dto">条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResultDto<APIManagementViewDto>> GetListAsync(ParameterAPIManagementDto dto)
            => await _apiManagementService.GetListAsync(dto);
        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="dto">新增信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultDto<bool>> CreateAsync(APIManagementCreateDto dto)
            => new ResultDto<bool> { Result = await _apiManagementService.InsertAsync(dto) };
        /// <summary>
        /// 修改接口信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="dto">修改信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultDto<bool>> UpdateAsync(Guid Id, APIManagementUpdateDto dto)
            => new ResultDto<bool> { Result = await _apiManagementService.UpdateAsync(Id, dto) };
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultDto<bool>> DeleteAsync(Guid Id)
            => new ResultDto<bool> { Result = await _apiManagementService.DeleteAsync(Id) };
        /// <summary>
        /// 开启或关闭接口
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultDto<bool>> OpenOrCloseAPIAsync(Guid Id)
            => new ResultDto<bool> { Result = await _apiManagementService.OpenOrCloseAPIAsync(Id) };

    }
}

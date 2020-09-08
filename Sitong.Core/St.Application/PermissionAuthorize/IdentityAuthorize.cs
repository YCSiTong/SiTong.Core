using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.PermissionAuthorize;
using St.Common.RedisCaChe;
using St.DoMain.Identity;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace St.Application.PermissionAuthorize
{
    /// <summary>
    /// 身份验证
    /// </summary>
    public class IdentityAuthorize : IIdentityAuthorize
    {
        private readonly IdentityInfo _identityInfo;
        private readonly IRepository<RoleAPIManagement, Guid> _roleAPIManagementRepository;
        private readonly IRepository<APIManagement, Guid> _apiManagementRepository;
        private readonly IRedisCaChe _redisCaChe;
        //private readonly IRepository<User, Guid> _userRepository;

        public IdentityAuthorize(
            IdentityInfo identityInfo
            , IRepository<RoleAPIManagement, Guid> roleAPIManagementRepository
            , IRepository<APIManagement, Guid> apiManagementRepository
            , IRedisCaChe redisCaChe
            //, IRepository<User, Guid> userRepository
            )
        {
            _identityInfo = identityInfo;
            _roleAPIManagementRepository = roleAPIManagementRepository;
            _apiManagementRepository = apiManagementRepository;
            _redisCaChe = redisCaChe;
            //_userRepository = userRepository;
        }

        public async Task<bool> IsPassAuthorize(string apiUrl)
        {
            var userId = _identityInfo.Identity.UId;
            var userRole = _identityInfo.Identity.Role;
            if (true)
            {

            }
            var roleModels = await _roleAPIManagementRepository.AsNoTracking().Where(op => userRole.Contains(op.RoleId)).Select(x => x.APIId).ToListAsync();// 查询所有角色接口权限
            var apiModels = await _apiManagementRepository.AsNoTracking().Where(op => op.ApiUrl == apiUrl).FirstOrDefaultAsync();// 查询当前访问的接口地址是否存在
            return roleModels.Where(op => op == apiModels.Id).Count().IsPositive(); // 是否存在该接口权限
        }
    }
}

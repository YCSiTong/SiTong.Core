using St.Application.Infrastruct.PermissionAuthorize;
using St.DoMain.Identity;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using System;
using System.Threading.Tasks;

namespace St.Application.PermissionAuthorize
{
    public class IdentityAuthorize : IIdentityAuthorize
    {
        private readonly IdentityInfo _identityInfo;
        private readonly IRepository<RoleMenu, Guid> _roleMenuRepository;

        public IdentityAuthorize(
            IdentityInfo identityInfo
            , IRepository<RoleMenu, Guid> roleMenuRepository
            )
        {
            _identityInfo = identityInfo;
            _roleMenuRepository = roleMenuRepository;
        }


        public async Task<bool> IsPassAuthorize(string apiUrl)
        {
            var userId = _identityInfo.Identity.UId;
            var userRole = _identityInfo.Identity.Role;
            throw new Exception("");
        }
    }
}

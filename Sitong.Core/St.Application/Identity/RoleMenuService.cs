using St.Application.Infrastruct.Identity;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using System;

namespace St.Application.Identity
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly IRepository<RoleMenu, Guid> _roleMenuRepository;
        public RoleMenuService(IRepository<RoleMenu, Guid> roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
        }
    }
}

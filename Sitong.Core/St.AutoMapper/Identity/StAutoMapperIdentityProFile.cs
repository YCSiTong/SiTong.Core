using AutoMapper;
using St.AutoMapper.Identity.APIManagement;
using St.AutoMapper.Identity.APIManagement.Regiter;
using St.AutoMapper.Identity.Menu;
using St.AutoMapper.Identity.Menu.Regiter;
using St.AutoMapper.Identity.Role;
using St.AutoMapper.Identity.Role.Regiter;
using St.AutoMapper.Identity.RoleAPIManagement;
using St.AutoMapper.Identity.RoleAPIManagement.Regiter;
using St.AutoMapper.Identity.RoleMenu;
using St.AutoMapper.Identity.RoleMenu.Regiter;
using St.AutoMapper.Identity.User;
using St.AutoMapper.Identity.User.Register;
using St.AutoMapper.Identity.UserRole;
using St.AutoMapper.Identity.UserRole.Register;

namespace St.AutoMapper.Identity
{
    /// <summary>
    /// 注入Identity自写权限Mapper映射
    /// </summary>
    public class StAutoMapperIdentityProFile : Profile
    {
        public StAutoMapperIdentityProFile()
        {
            //User
            CreateMap<DoMain.Model.Identity.User, UserViewDto>();
            CreateMap<UserCreateDto, DoMain.Model.Identity.User>();
            CreateMap<UserUpdateDto, DoMain.Model.Identity.User>();

            //Role
            CreateMap<DoMain.Model.Identity.Role, RoleViewDto>();
            CreateMap<RoleCreateDto, DoMain.Model.Identity.Role>();
            CreateMap<RoleUpdateDto, DoMain.Model.Identity.Role>();

            //UserRole
            CreateMap<DoMain.Model.Identity.UserRole, UserRoleViewDto>();
            CreateMap<UserRoleCreateDto, DoMain.Model.Identity.UserRole>();
            CreateMap<UserRoleUpdateDto, DoMain.Model.Identity.UserRole>();

            //RoleMenu
            CreateMap<DoMain.Model.Identity.RoleMenu, RoleMenuViewDto>();
            CreateMap<RoleMenuCreateDto, DoMain.Model.Identity.RoleMenu>();
            CreateMap<RoleMenuUpdateDto, DoMain.Model.Identity.RoleMenu>();

            //Menu
            CreateMap<DoMain.Model.Identity.Menu, MenuViewDto>();
            CreateMap<MenuCreateDto, DoMain.Model.Identity.Menu>();
            CreateMap<MenuUpdateDto, DoMain.Model.Identity.Menu>();

            //APIManagement
            CreateMap<DoMain.Model.Identity.APIManagement, APIManagementViewDto>();
            CreateMap<APIManagementCreateDto, DoMain.Model.Identity.APIManagement>();
            CreateMap<APIManagementUpdateDto, DoMain.Model.Identity.APIManagement>();

            //RoleAPIManagement
            CreateMap<DoMain.Model.Identity.RoleAPIManagement, RoleAPIManagementViewDto>();
            CreateMap<RoleAPIManagementCreateDto, DoMain.Model.Identity.RoleAPIManagement>();
            CreateMap<RoleAPIManagementUpdateDto, DoMain.Model.Identity.RoleAPIManagement>();
        }
    }
}

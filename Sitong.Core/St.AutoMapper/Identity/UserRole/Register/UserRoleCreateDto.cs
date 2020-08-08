using System;

namespace St.AutoMapper.Identity.UserRole.Register
{
    public class UserRoleCreateDto
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }
    }
}

using System;

namespace St.AutoMapper.Identity.UserRole
{
    public class UserRoleViewDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
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

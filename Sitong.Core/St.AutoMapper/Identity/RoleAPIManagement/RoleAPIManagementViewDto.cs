using System;

namespace St.AutoMapper.Identity.RoleAPIManagement
{
    public class RoleAPIManagementViewDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 接口编号
        /// </summary>
        public Guid APIId { get; set; }
    }
}

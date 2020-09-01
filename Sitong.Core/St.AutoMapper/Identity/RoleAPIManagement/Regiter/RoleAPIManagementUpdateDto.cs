using System;

namespace St.AutoMapper.Identity.RoleAPIManagement.Regiter
{
    public class RoleAPIManagementUpdateDto
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

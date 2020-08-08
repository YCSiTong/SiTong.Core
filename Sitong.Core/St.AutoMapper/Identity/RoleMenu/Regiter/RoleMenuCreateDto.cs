using System;

namespace St.AutoMapper.Identity.RoleMenu.Regiter
{
    public class RoleMenuCreateDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public Guid MenuId { get; set; }
    }
}

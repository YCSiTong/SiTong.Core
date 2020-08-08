using System;

namespace St.AutoMapper.Identity.RoleMenu
{
    internal class RoleMenuViewDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
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

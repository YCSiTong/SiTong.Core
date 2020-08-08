using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    /// <summary>
    /// 角色拥有菜单
    /// </summary>
    public class RoleMenu : FullAudited<Guid>
    {
        public RoleMenu()
        {
            Id = GuidAll.NewGuid();
        }

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

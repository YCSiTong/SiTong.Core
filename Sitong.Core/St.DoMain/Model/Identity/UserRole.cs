using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    /// <summary>
    /// 管理员拥有角色
    /// </summary>
    public class UserRole : FullAudited<Guid>
    {
        public UserRole()
        {
            Id = GuidAll.NewGuid();
        }
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

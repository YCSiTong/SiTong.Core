using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    public class RoleAPIManagement : FullAudited<Guid>
    {
        public RoleAPIManagement()
        {
            Id = GuidAll.NewGuid();
        }

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

using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : FullAudited<Guid>
    {
        public Role()
        {
            Id = GuidAll.NewGuid();
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }



    }
}

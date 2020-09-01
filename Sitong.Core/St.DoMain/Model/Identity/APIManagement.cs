using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    public class APIManagement : FullAudited<Guid>
    {
        public APIManagement()
        {
            Id = GuidAll.NewGuid();
        }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}

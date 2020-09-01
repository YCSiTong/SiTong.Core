using System;

namespace St.AutoMapper.Identity.APIManagement
{
    public class APIManagementViewDto
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public Guid Id { get; set; }
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
    }
}

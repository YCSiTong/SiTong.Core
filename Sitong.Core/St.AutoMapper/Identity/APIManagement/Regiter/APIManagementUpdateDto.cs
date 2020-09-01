using System.ComponentModel.DataAnnotations;

namespace St.AutoMapper.Identity.APIManagement.Regiter
{
    public class APIManagementUpdateDto
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        [Required]
        public string ApiUrl { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}

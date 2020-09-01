using System;
using System.Collections.Generic;
using System.Text;

namespace St.AutoMapper.Identity.RoleAPIManagement.Regiter
{
    public class RoleAPIManagementCreateDto
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

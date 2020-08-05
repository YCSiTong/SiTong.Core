using St.DoMain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace St.Model.Identity
{
    /// <summary>
    /// 管理员基础信息
    /// </summary>
    public class User : IAggregateRoot<Guid>
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// QQ邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPic { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 双因子身份验证是否开启
        /// </summary>
        public bool TwoFactorVerifyEnable { get; set; }
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze { get; set; }


        public Guid CreatorId { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModifierTime { get; set; }
        public bool IsDeleted { get; set; }
        Guid IAggregateRoot<Guid>.Id { get; set; }
    }
}

using St.Common.GuidMethods;
using St.DoMain.Entity.Full;
using System;

namespace St.DoMain.Model.Identity
{
    /// <summary>
    /// 管理员基础信息
    /// </summary>
    public class User : FullAudited<Guid>
    {
        public User()
        {
            Id = GuidAll.NewGuid();
        }

        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }
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

    }
}

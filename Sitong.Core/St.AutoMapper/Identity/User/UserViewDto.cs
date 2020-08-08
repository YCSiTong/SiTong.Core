using System;

namespace St.AutoMapper.Identity.User
{
    public class UserViewDto
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }
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
    }
}

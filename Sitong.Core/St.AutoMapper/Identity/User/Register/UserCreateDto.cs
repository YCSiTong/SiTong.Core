namespace St.AutoMapper.Identity.User.Register
{
    public class UserCreateDto
    {
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
    }
}

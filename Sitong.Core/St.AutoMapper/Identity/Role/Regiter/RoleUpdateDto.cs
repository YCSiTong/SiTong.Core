namespace St.AutoMapper.Identity.Role.Regiter
{
    public class RoleUpdateDto
    {
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

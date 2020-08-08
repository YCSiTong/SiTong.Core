using St.DoMain.Model.Identity.Enum;
using System;

namespace St.AutoMapper.Identity.Menu.Regiter
{
    public class MenuUpdateDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Icon图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 上级主键
        /// </summary>
        public Guid SuperiorId { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuTypeEnum MenuType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}

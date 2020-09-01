using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class MenuModelBuilder
    {
        /// <summary>
        /// 菜单表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseMenuEntity(this ModelBuilder builder)
        {
            builder.Entity<Menu>(op =>
            {
                op.ToTable("Menu");
                op.HasKey(x => x.Id);
                op.Property(x => x.Icon).IsRequired().HasColumnType("varchar(20)").HasComment("ICon矢量图标");
                op.Property(x => x.Name).IsRequired().HasColumnType("varchar(30)").HasComment("菜单名称");
                op.Property(x => x.Url).IsRequired().HasColumnType("varchar(50)").HasComment("ICon矢量图标");
                op.Property(x => x.SuperiorId).IsRequired().HasColumnType("varchar(100)").HasComment("父级编号");
                op.Property(x => x.OrderId).IsRequired().HasColumnType("int").HasComment("排列顺序");
                op.Property(x => x.Description).IsRequired().HasColumnType("varchar(200)").HasComment("详细说明");
                op.Property(x => x.IsLock).IsRequired().HasColumnType("bit").HasComment("是否锁定");
                op.Property(x => x.MenuType).IsRequired().HasColumnType("int").HasComment("菜单类型 2模块 4方法/接口一类");

            });
        }
    }
}

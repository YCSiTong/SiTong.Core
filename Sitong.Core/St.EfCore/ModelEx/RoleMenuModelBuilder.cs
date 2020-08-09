using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class RoleMenuModelBuilder
    {
        /// <summary>
        /// 角色菜单表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseRoleMenuEntity(this ModelBuilder builder)
        {
            builder.Entity<RoleMenu>(op =>
            {
                op.ToTable("RoleMenu");
                op.HasKey(x => x.Id);
                op.Property(x => x.RoleId).IsRequired().HasColumnType("uniqueidentifier").HasComment("角色编号");
                op.Property(x => x.MenuId).IsRequired().HasColumnType("uniqueidentifier").HasComment("菜单编号");
            });
        }
    }
}

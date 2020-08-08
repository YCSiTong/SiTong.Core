using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class UserRoleModelBuilder
    {
        /// <summary>
        /// 管理员角色管理表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseUserRoleEntity(this ModelBuilder builder)
        {
            builder.Entity<UserRole>(op =>
            {
                op.ToTable("UserRole");
                op.HasKey(x => x.Id);
                op.Property(x => x.UserId).IsRequired().HasColumnType("uniqueidentifier").HasComment("管理员主键");
                op.Property(x => x.RoleId).IsRequired().HasColumnType("uniqueidentifier").HasComment("角色主键");
            });
        }
    }
}

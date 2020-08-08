using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class RoleModelBuilder
    {
        /// <summary>
        /// 角色表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseRoleEntity(this ModelBuilder builder)
        {
            builder.Entity<Role>(op =>
            {
                op.ToTable("Role");
                op.HasKey(x => x.Id);
                op.Property(x => x.Name).IsRequired().HasColumnType("varchar(40)").HasComment("角色名称");
                op.Property(x => x.IsAdmin).IsRequired().HasColumnType("bit").HasComment("是否管理员 True=是/Flase=不是");
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class RoleAPIManagementModelBuilder
    {
        /// <summary>
        /// 角色API表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseRoleAPIManagementEntity(this ModelBuilder builder)
        {
            builder.Entity<RoleAPIManagement>(op =>
            {
                op.ToTable("RoleAPIManagement");
                op.HasKey(x => x.Id);
                op.Property(x => x.RoleId).IsRequired().HasColumnType("uniqueidentifier").HasComment("角色编号");
                op.Property(x => x.APIId).IsRequired().HasColumnType("uniqueidentifier").HasComment("接口编号");
            });
        }
    }
}

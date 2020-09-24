using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class APIManagementModelBuilder
    {
        /// <summary>
        /// API接口管理
        /// </summary>
        /// <param name="builder"></param>
        public static void UseAPIManagementEntity(this ModelBuilder builder)
        {
            builder.Entity<APIManagement>(op =>
            {
                op.ToTable("APIManagement");
                op.HasKey(x => x.Id);
                op.Property(x => x.Name).IsRequired().HasColumnType("varchar(30)").HasComment("功能名称");
                op.Property(x => x.ApiUrl).IsRequired().HasColumnType("varchar(50)").HasComment("接口地址");
                op.Property(x => x.Description).IsRequired().HasColumnType("varchar(200)").HasComment("详细说明");
                op.Property(x => x.IsEnabled).IsRequired().HasColumnType("bit").HasComment("是否启用");
            });
        }
    }
}

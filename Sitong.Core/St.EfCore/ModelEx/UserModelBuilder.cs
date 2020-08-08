using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore.ModelEx
{
    public static class UserModelBuilder
    {
        /// <summary>
        /// 管理员用户表
        /// </summary>
        /// <param name="builder"></param>
        public static void UseUserEntity(this ModelBuilder builder)
        {
            builder.Entity<User>(op =>
            {
                op.ToTable("User");
                op.HasKey(x => x.Id);
                op.Property(x => x.Account).IsRequired().HasColumnType("varchar(50)").HasComment("账户名");
                op.Property(x => x.PassWord).IsRequired().HasColumnType("varchar(40)").HasComment("密码");
                op.Property(x => x.NickName).IsRequired().HasColumnType("varchar(26)").HasComment("昵称");
                op.Property(x => x.Sex).IsRequired().HasColumnType("bit").HasComment("性别,True=男/False=女");
                op.Property(x => x.TwoFactorVerifyEnable).IsRequired().HasColumnType("bit").HasComment("是否开启双因子验证");
                op.Property(x => x.PhoneNumber).IsRequired().HasColumnType("varchar(20)").HasComment("手机号码");
                op.Property(x => x.Email).IsRequired().HasColumnType("varchar(40)").HasComment("邮箱账户");
                op.Property(x => x.IsFreeze).IsRequired().HasColumnType("bit").HasComment("是否冻结,True=冻结/False=未冻结");
                op.Property(x => x.HeadPic).HasColumnType("varchar(150)").HasComment("用户头像");
            });
        }
    }
}

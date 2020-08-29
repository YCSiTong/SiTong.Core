using Microsoft.EntityFrameworkCore;
using St.DoMain.Model.Identity;

namespace St.EfCore
{
    public class StDbContext : DbContext
    {
        public StDbContext(DbContextOptions<StDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.ModelSourceCreating();// 自定义CodeFirst扩展
        }
    }
}

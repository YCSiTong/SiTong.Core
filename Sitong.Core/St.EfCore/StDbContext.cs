using Microsoft.EntityFrameworkCore;

namespace St.EfCore
{
    public class StDbContext : DbContext
    {
        public StDbContext(DbContextOptions<StDbContext> options) : base(options)
        {

        }

        #region DbSet

        #endregion

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.ModelSourceCreating();//自定义CodeFirst扩展
        }
    }
}

using Microsoft.EntityFrameworkCore;
using St.EfCore;

namespace St.Host.API.CodeFirst.EntityFrameWorkCore
{
    public class StMigrationDbContext : DbContext
    {
        public StMigrationDbContext(DbContextOptions<StMigrationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.ModelSourceCreating();// 自定义CodeFirst扩展
        }
    }
}

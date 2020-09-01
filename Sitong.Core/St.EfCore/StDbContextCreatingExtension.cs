using Microsoft.EntityFrameworkCore;
using St.EfCore.ModelEx;

namespace St.EfCore
{
    /// <summary>
    /// CodeFitstExtensions
    /// </summary>
    public static class StDbContextCreatingExtension
    {
        /// <summary>
        /// CodeFirst扩展
        /// </summary>
        /// <param name="builder"></param>
        public static void ModelSourceCreating(this ModelBuilder builder)
        {
            builder.UseUserEntity();
            builder.UseUserRoleEntity();
            builder.UseRoleEntity();
            builder.UseRoleMenuEntity();
            builder.UseMenuEntity();
            builder.UseAPIManagementEntity();
            builder.UseRoleAPIManagementEntity();
        }
    }
}

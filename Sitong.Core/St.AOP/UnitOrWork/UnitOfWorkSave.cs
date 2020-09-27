using AspectCore.DynamicProxy;
using St.DoMain.UnitOfWork;
using System.Threading.Tasks;

namespace St.AOP.UnitOrWork
{
    /// <summary>
    /// 全面事务启用
    /// </summary>
    public class UnitOfWorkSave : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var unitOfWork = (IUnitOfWork)context.ServiceProvider.GetService(typeof(IUnitOfWork));
            await unitOfWork.UseTransactionAsync(async () =>
            {
                await next(context);
            });
        }
    }
}

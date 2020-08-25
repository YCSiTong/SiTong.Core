using St.Common.Helper;
using System.Threading.Tasks;

namespace St.DoMain.Identity
{
    /// <summary>
    /// 身份信息
    /// </summary>
    public interface IdentityInfo
    {
        /// <summary>
        /// 身份信息实体
        /// </summary>
        IdentityModel Identity { get; }

        
    }
}

using St.Common.Helper;
using St.DoMain.Identity;

namespace St.DoMain.Core.Identity
{
    /// <summary>
    /// 身份信息解析实现
    /// </summary>
    public class IdentityInfoRealize : IdentityInfo
    {
        public IdentityInfoRealize(IdentityModel identity)
        {
            Identity = identity;
        }

        public IdentityModel Identity { get; private set; }
    }
}

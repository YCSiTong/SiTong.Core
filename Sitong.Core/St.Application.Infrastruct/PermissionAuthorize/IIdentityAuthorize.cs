using System.Threading.Tasks;

namespace St.Application.Infrastruct.PermissionAuthorize
{
    /// <summary>
    /// 权限校验
    /// </summary>
    public interface IIdentityAuthorize
    {
        /// <summary>
        /// 是否拥有对应接口权限
        /// </summary>
        /// <param name="apiUrl">访问的api接口地址</param>
        /// <returns></returns>
        Task<bool> IsPassAuthorize(string apiUrl);
    }
}

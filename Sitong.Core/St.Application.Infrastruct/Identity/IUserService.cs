using St.AutoMapper.Common;
using St.AutoMapper.Identity.User;
using St.AutoMapper.Identity.User.Register;
using System;
using System.Threading.Tasks;

namespace St.Application.Infrastruct.Identity
{
    public interface IUserService
    {
        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        Task<UserViewDto> LoginAsync(string account, string passWord);
        /// <summary>
        /// 分页所有管理员账户
        /// </summary>
        /// <param name="parDto">请求参数</param>
        /// <returns></returns>
        Task<PageResultDto<UserViewDto>> GetListAsync(ParameterUserDto parDto);
        /// <summary>
        /// 开启管理员冻结
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> OpenLockAsync(Guid Id);
        /// <summary>
        /// 解除管理员冻结
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> UnLockAsync(Guid Id);
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id);
        /// <summary>
        /// 新增管理员信息
        /// </summary>
        /// <param name="dto">新增的信息</param>
        /// <returns></returns>
        Task<bool> InsertAsync(UserCreateDto dto);
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改的内容</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, UserUpdateDto dto);
    }
}

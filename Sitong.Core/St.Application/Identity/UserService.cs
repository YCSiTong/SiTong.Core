using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.User;
using St.Common.Helper;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Exceptions;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace St.Application.Identity
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _userRepository;
        public UserService(IRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public async Task<UserViewDto> LoginAsync(string account, string passWord)
        {
            var userDto = (await _userRepository.AsNoTracking()
                .Where(op => op.Account == account && op.PassWord == MD5Helper.MD5Encrypt32(passWord))
                .FirstOrDefaultAsync()).ToMap<UserViewDto>();

            if (userDto.IsNotNull())
                return userDto;
            else
                throw new BusinessException("当前账户名或密码输入错误!!!");
        }

        /// <summary>
        /// 分页所有管理员账户
        /// </summary>
        /// <param name="parDto">请求参数</param>
        /// <returns></returns>
        public async Task<PageResultDto<IEnumerable<UserViewDto>>> GetListAsync(ParameterUserDto parDto)
        {
            var userDtos = (await _userRepository.AsNoTracking().Page(parDto.SkipCount, parDto.MaxResultCount).ToListAsync()).ToMap<UserViewDto>();
            var userTotalCount = await _userRepository.AsNoTracking().CountAsync();
            return new PageResultDto<IEnumerable<UserViewDto>> { TotalCount = userTotalCount, Result = userDtos };
        }

        /// <summary>
        /// 开启管理员冻结
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> OpenLockAsync(Guid Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if (user.IsNull())
                throw new BusinessException("当前管理员账户异常,请刷新!!!");
            user.IsFreeze = true;
            return await _userRepository.UpdataAsync(user);
        }

        /// <summary>
        /// 解除管理员冻结
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(Guid Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if (user.IsNull())
                throw new BusinessException("当前管理员账户异常,请刷新!!!");
            user.IsFreeze = false;
            return await _userRepository.UpdataAsync(user);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
           => await _userRepository.DeleteAsync(Id);

    }
}

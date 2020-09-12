using Microsoft.EntityFrameworkCore;
using St.Application.Infrastruct.Identity;
using St.AutoMapper.Common;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity.User;
using St.AutoMapper.Identity.User.Register;
using St.Common.Helper;
using St.DoMain.Model.Identity;
using St.DoMain.Repository;
using St.Exceptions;
using St.Extensions;
using System;
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
            account.NotEmptyOrNull(nameof(account));
            passWord.NotEmptyOrNull(nameof(passWord));
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
        public async Task<PageResultDto<UserViewDto>> GetListAsync(ParameterUserDto parDto)
        {
            parDto.NotNull(nameof(ParameterUserDto));
            var userDtos = (await _userRepository.AsNoTracking().Page(parDto.SkipCount, parDto.MaxResultCount).ToListAsync()).ToMap<UserViewDto>();
            var userTotalCount = await _userRepository.AsNoTracking().CountAsync();
            return new PageResultDto<UserViewDto> { TotalCount = userTotalCount, Result = userDtos };
        }

        /// <summary>
        /// 开启管理员冻结
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> OpenOrCloseFreezeAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            var user = await _userRepository.GetByIdAsync(Id);
            if (user.IsNull())
                throw new BusinessException("当前管理员账户异常,请刷新后重试!!!");
            if (user.IsFreeze)
                user.IsFreeze = false;
            else
                user.IsFreeze = true;
            return await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid Id)
        {
            Id.NotEmpty(nameof(Id));
            return await _userRepository.DeleteAsync(Id);
        }
        /// <summary>
        /// 新增管理员信息
        /// </summary>
        /// <param name="dto">新增的信息</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(UserCreateDto dto)
        {
            dto.NotNull(nameof(UserCreateDto));
            var userModel = dto.ToMap<User>();
            userModel.PassWord = MD5Helper.MD5Encrypt32(userModel.PassWord);
            return await _userRepository.InsertAsync(userModel);
        }
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="dto">修改的内容</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid Id, UserUpdateDto dto)
        {
            Id.NotEmpty(nameof(Id));
            dto.NotNull(nameof(UserUpdateDto));

            var userModel = await _userRepository.GetByIdAsync(Id);
            if (userModel.IsNotNull())
            {
                var userResult = dto.ToMap(userModel);
                return await _userRepository.UpdateAsync(userResult);

            }
            throw new BusinessException("当前修改管理员信息异常,请刷新重试!!!");
        }
    }
}

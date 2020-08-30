using System;
using System.Collections.Generic;
using System.Text;

namespace St.Exceptions.Redis
{
    /// <summary>
    /// Redis异常信息
    /// </summary>
    public class RedisParametersException : BusinessException
    {
        public RedisParametersException(string message) : base($"方法 {{{message}}} 在其服务内部调用参数异常！")
        {

        }
    }
}

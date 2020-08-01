using System;

namespace St.Exceptions
{
    /// <summary>
    /// 自定义业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}

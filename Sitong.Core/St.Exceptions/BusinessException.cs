using System;

namespace St.Exceptions
{
    /// <summary>
    /// 自定义业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public readonly string Code;

        public BusinessException(string message, string code = "5001") : base(message)
        {
            Code = code;
        }
    }
}

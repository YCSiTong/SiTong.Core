using System;

namespace St.Common.Attributes
{
    /// <summary>
    /// 该方法使用缓存
    /// </summary>
    public class StCaCheAttribute : Attribute
    {
        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int ExpirationTime { get; set; } = 10;

    }
}

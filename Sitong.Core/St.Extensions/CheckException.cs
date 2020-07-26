using St.Exceptions;
using System;

namespace St.Extensions
{
    public static class CheckException
    {
        /// <summary>
        /// 验证<paramref name="verify"/>是否为真,否则抛出<paramref name="message"/>指定类型<typeparamref name="TEx"/>异常
        /// </summary>
        /// <typeparam name="TEx"></typeparam>
        /// <param name="verify"></param>
        /// <param name="message"></param>
        private static void ThrowEx<TEx>(bool verify, string message) where TEx : Exception
        {
            if (verify)
                return;

            if (string.IsNullOrEmpty(message))
                throw new BusinessException(nameof(message));

            TEx exception = (TEx)Activator.CreateInstance(typeof(TEx), message);
            throw exception;
        }

        /// <summary>
        /// 验证对象<typeparamref name="T"/>是否为Null,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">对象</param>
        /// <param name="paramName">对象名</param>
        public static void NotNull<T>(this T val, string paramName) where T : class
        {
            ThrowEx<ArgumentException>(val != null, $"对象 <{paramName}> 不能为空引用");
        }
        /// <summary>
        /// 验证参数类型<see cref="string"/>值是否为空或Null,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="paramName">参数名称</param>
        public static void NotEmptyOrNull(this string val, string paramName)
        {
            ThrowEx<ArgumentException>(!string.IsNullOrEmpty(val), $"参数 <{paramName}> 不能为Empty或Null");
        }
        /// <summary>
        /// 验证参数类型<see cref="Guid"/>值是否为空,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="val">Guid</param>
        /// <param name="paramName">Guid名称</param>
        public static void NotEmpty(this Guid val, string paramName)
        {
            ThrowEx<ArgumentException>(Guid.Empty != val, $"参数 <{paramName}> 不能为Guid.Empty");
        }
        /// <summary>
        /// 验证参数类型<see cref="int"/>值是否正整数,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="val">Int</param>
        /// <param name="paramName">参数名称</param>
        public static void IsPositive(this int val, string paramName)
        {
            ThrowEx<ArgumentException>(val >= 0, $"参数 <{paramName}> 值必须为正整数");
        }
        /// <summary>
        /// 验证参数类型<see cref="int"/>值是否负整数,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <param name="val">Int</param>
        /// <param name="paramName">参数名称</param>
        public static void IsMinus(this int val, string paramName)
        {
            ThrowEx<ArgumentException>(val < 0, $"参数 <{paramName}> 值必须为负整数");
        }
        /// <summary>
        /// 验证参数类型<see cref="string"/>[]是否至少具有一个值,抛出<see cref="ArgumentException"/>
        /// </summary>
        /// <param name="val">string[]</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckArray(this string[] val, string paramName)
        {
            ThrowEx<ArgumentException>(val.Length > 0, $"参数<{paramName}>至少有1个以上值");
        }







    }
}

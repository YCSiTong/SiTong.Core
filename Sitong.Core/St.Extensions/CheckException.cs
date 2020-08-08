using System;
using System.Collections.Generic;

namespace St.Extensions
{
    /// <summary>
    /// 检查参数抛出异常
    /// </summary>
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
            if (verify) return;

            if (message.IsNullOrEmpty())
                throw new AggregateException($"校验抛出信息为空,但校验结果异常.");

            TEx exception = (TEx)Activator.CreateInstance(typeof(TEx), message);
            throw exception;
        }

        /// <summary>
        /// 自定义校验,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">对象</param>
        /// <param name="verify">校验条件</param>
        /// <param name="exMsg">异常信息</param>
        public static void CustomVerify(bool verify, string exMsg)
        {
            ThrowEx<AggregateException>(verify, exMsg);
        }

        /// <summary>
        /// 验证对象<typeparamref name="T"/>是否为Null,抛出<see cref="ArgumentException"/>异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">对象</param>
        /// <param name="paramName">对象名</param>
        public static void NotNull<T>(this T val, string paramName) where T : class
        {
            ThrowEx<ArgumentException>(val != null, $"对象 <{paramName}> 不能为Null");
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

        /// <summary>
        /// 验证参数类型<see cref="List{T}"/>是否至少具有一个值或不为Null,抛出<see cref="ArgumentException"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">List</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckList<T>(this List<T> val, string paramName)
        {
            ThrowEx<ArgumentException>(val.Count == 0 || val == null, $"参数<{paramName}>Count为0或本身Null");
        }


    }
}

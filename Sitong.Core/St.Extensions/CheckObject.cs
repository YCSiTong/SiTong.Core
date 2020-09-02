using System;
using System.Reflection;

namespace St.Extensions
{
    /// <summary>
    /// 检查参数是否合格
    /// </summary>
    public static class CheckObject
    {
        /// <summary>
        /// 判断<typeparamref name="T"/>对象是否为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T val) => val == null;

        /// <summary>
        /// 判断<typeparamref name="T"/>对象是否不为Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T val) => !val.IsNull();

        /// <summary>
        /// 判断<see cref="string"/>是否为Empty或Null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string val) => string.IsNullOrEmpty(val);
        /// <summary>
        /// 判断ToString()后是否为Empty或Null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object val) => val.ToString().IsNullOrEmpty();

        /// <summary>
        /// 判断<see cref="string"/>是否不为Empty或Null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this string val) => !val.IsNullOrEmpty();
        /// <summary>
        /// 判断ToString()后是否不为Empty或Null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this object val) => val.ToString().IsNotEmptyOrNull();

        /// <summary>
        /// 判断<see cref="int"/>是否大于等于0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsPositive(this int val) => val >= 0;

        /// <summary>
        /// 判断<see cref="int"/>是否小于0或不等于0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsMinus(this int val) => !val.IsPositive();

        /// <summary>
        /// 判断是否包含<typeparamref name="TAttribute"/>特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo memberInfo)
            where TAttribute : Attribute
            => memberInfo.IsDefined(typeof(TAttribute), true);



    }
}

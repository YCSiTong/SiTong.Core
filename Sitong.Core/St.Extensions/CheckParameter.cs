﻿namespace St.Extensions
{
    /// <summary>
    /// 检查参数是否合格
    /// </summary>
    public static class CheckParameter
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
        /// 判断<see cref="string"/>是否不为Empty或Null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this string val) => !val.IsNotEmptyOrNull();

        /// <summary>
        /// 判断<see cref="int"/>是否 >= 0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsPositive(this int val) => val >= 0;

        /// <summary>
        /// 判断<see cref="int"/>是否 < 0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsMinus(this int val) => !val.IsPositive();


    }
}

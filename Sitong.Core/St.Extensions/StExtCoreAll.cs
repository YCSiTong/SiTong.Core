using System;
using System.Linq;

namespace St.Extensions
{
    public static class StExtCoreAll
    {

        /// <summary>
        /// 是否被单/多个对象包含着
        /// </summary>
        /// <param name="item">需要判断的对象</param>
        /// <param name="list">判断数组</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>是否包含在内</returns>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
        /// <summary>
        /// 符合条件后可执行委托
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="condition">条件</param>
        /// <param name="func">委托</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>当前执行委托后返回值/当前对象</returns>
        public static T If<T>(this T obj, bool condition, Func<T, T> func)
        {
            if (condition)
            {
                return func(obj);
            }

            return obj;
        }

        /// <summary>
        /// 符合条件后可执行委托
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="condition">条件</param>
        /// <param name="action">委托</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>当前执行委托后返回值/当前对象</returns>
        public static T If<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }

            return obj;
        }
    }
}

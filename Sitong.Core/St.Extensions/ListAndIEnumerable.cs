using System;
using System.Collections.Generic;
using System.Linq;

namespace St.Extensions
{
    public static class ListAndIEnumerable
    {
        /// <summary>
        /// IEnumerable扩展ForEach方法
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <param name="list">源数据</param>
        /// <param name="action">委托</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            list.NotNull(nameof(IEnumerable<T>));
            foreach (var item in list)
            {
                action(item);
            }
        }
        /// <summary>
        /// IEnumerable扩展ForEach方法
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <param name="list">源数据</param>
        /// <param name="action">委托</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            list.NotNull(nameof(IEnumerable<T>));
            var array = list.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }
    }
}

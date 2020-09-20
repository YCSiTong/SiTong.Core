using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
        /// <summary>
        /// <see cref="IEnumerable{T}"/>扩展根据指定字符以及分隔符获取拼接字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">值</param>
        /// <param name="left">左侧符号</param>
        /// <param name="right">右侧符号</param>
        /// <param name="split">分割符</param>
        /// <returns></returns>
        public static string ToSqlIn<T>(this IEnumerable<T> list, string left = "'", string right = "'", string split = ",")
        {
            list.NotNull(nameof(IEnumerable<T>));
            StringBuilder builder = new StringBuilder();
            list.ForEach(x =>
            {
                builder.Append(left + x + right + split);
            });
            return builder.ToString().Substring(0, builder.Length - 1);
        }
        ///// <summary>
        ///// <see cref="IEnumerable{T}"/>扩展根据返回指定键的列表信息
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <typeparam name="Key">键类型</typeparam>
        ///// <param name="list">所有值</param>
        ///// <param name="key">指定键</param>
        ///// <returns></returns>
        //public static IEnumerable<Key> ToKeys<T, Key>(this IEnumerable<T> list, Expression<Func<T, Key>> key)
        //{
        //    foreach (var item in list)
        //    {
        //    }
        //}
        /// <summary>
        /// <see cref="IEnumerable{T}"/>扩展转换全部类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> AsToAll<T, TResult>(this IEnumerable<T> list)
        {
            list.NotNull(nameof(IEnumerable<T>));
            var result = list as IEnumerable<TResult>;
            if (result.IsNotNull())
            {
                return result;
            }
            return YieldAsTo<T, TResult>(list);

        }
        private static IEnumerable<TResult> YieldAsTo<T, TResult>(this IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                yield return item.AsTo<TResult>();
            }
            yield break;
        }
    }
}

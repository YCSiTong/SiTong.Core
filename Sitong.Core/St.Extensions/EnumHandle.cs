using System;
using System.Collections.Generic;
using System.Text;

namespace St.Extensions
{
    public static class EnumHandle
    {
        /// <summary>
        /// 根据<see cref="Enum"/>获取所有属性
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="val">枚举</param>
        /// <returns></returns>
        public static IEnumerable<T> GetIEnumAll<T>(this Enum val)
        {
            val.NotNull(nameof(Enum));
            foreach (var item in Enum.GetValues(val.GetType()))
            {
                yield return (T)item;
            }
        }



    }
}

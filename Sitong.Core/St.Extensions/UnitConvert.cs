using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace St.Extensions
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static class UnitConvert
    {
        /// <summary>
        /// obj转换为bool类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ToBool(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && bool.TryParse(val.ToString(), out bool result))
                return result;
            else
                return false;
        }
        /// <summary>
        /// obj转换为Int类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && int.TryParse(val.ToString(), out int result))
                return result;
            else
                return -1;
        }
        /// <summary>
        /// obj转换为Double类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double ToDouble(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && double.TryParse(val.ToString(), out double result))
                return result;
            else
                return -1;
        }
        /// <summary>
        /// obj转换为Decimal类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && decimal.TryParse(val.ToString(), out decimal result))
                return result;
            else
                return -1;
        }
        /// <summary>
        /// obj转换为Date类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && DateTime.TryParse(val.ToString(), out DateTime result))
                return result;
            else
                return DateTime.MinValue;
        }
        /// <summary>
        /// obj转换为Guid类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value && Guid.TryParse(val.ToString(), out Guid guid))
                return guid;
            else
                return Guid.Empty;
        }

        /// <summary>
        /// obj序列化成Json格式字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToJson(this object val)
        {
            return JsonConvert.SerializeObject(val);
        }

        public static T ToEntity<T>(this object val)
        {
            return JsonConvert.DeserializeObject<T>(val.ToString());
        }

        /// <summary>
        /// 将源数据转换为指定类型源数据
        /// </summary>
        /// <param name="val">源数据</param>
        /// <param name="type">目标类型</param>
        /// <returns></returns>
        private static object AsTo(object val, Type type)
        {
            if (val.IsNull() || val is DBNull)
                return default;

            // 判断Nullable<>
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                NullableConverter nullableConverter = new NullableConverter(type);// 获取Nullable类型的基础类型
                return Convert.ChangeType(val, nullableConverter.UnderlyingType);
            }

            // 判断Enum
            if (type.IsEnum)
            {
                return Enum.Parse(type, val.ToString());
            }

            // 判断Guid
            if (type == typeof(Guid))
            {
                return val.ToGuid();
            }

            return Convert.ChangeType(val, type);
        }
        /// <summary>
        /// 将源数据转换为指定类型源数据
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <typeparam name="TResult">目标类型</typeparam>
        /// <param name="val">源数据</param>
        /// <returns></returns>
        public static TResult AsTo<TResult>(this object val) where TResult : class
        {
            return (TResult)AsTo(val, typeof(TResult));
        }


    }
}

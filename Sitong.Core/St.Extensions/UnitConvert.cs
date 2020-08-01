using System;

namespace St.Extensions
{
    public static class UnitConvert
    {
        /// <summary>
        /// obj转换为bool类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ToBool(this object val)
        {
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                bool.TryParse(val.ToString(), out bool result);
                return result;
            }
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
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                int.TryParse(val.ToString(), out int result);
                return result;
            }
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
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                double.TryParse(val.ToString(), out double result);
                return result;
            }
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
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                decimal.TryParse(val.ToString(), out decimal result);
                return result;
            }
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
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                DateTime.TryParse(val.ToString(), out DateTime result);
                return result;
            }
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
            if (val.ToString().Length > 0 && val != DBNull.Value)
            {
                Guid.TryParse(val.ToString(), out Guid guid);
                return guid;
            }
            else
                return Guid.Empty;
        }

    }
}

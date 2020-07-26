using St.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace St.Common.Helper
{
    public class MD5Helper
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Created16(string str)
        {
            str.NotEmptyOrNull(nameof(str));

            using var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", string.Empty);
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str">需要转换的字符</param>
        /// <returns></returns>
        public static string MD5Encrypt32(string str)
        {
            str.NotEmptyOrNull(nameof(str));

            string resultStr = string.Empty;
            using MD5 md5 = MD5.Create(); //实例化一个md5对像
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (var item in s)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                resultStr = string.Concat(resultStr, item.ToString("X"));
            }
            return resultStr;
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="str">需要转换的字符</param>
        /// <returns></returns>
        public static string MD5Encrypt64(string str)
        {
            str.NotEmptyOrNull(nameof(str));

            using MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(s);
        }
    }
}

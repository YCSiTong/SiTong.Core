using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Linq;

namespace St.Common.Helper
{
    public class AppSettings
    {
        private static IConfiguration _Configuration;

        public AppSettings(string Path)
        {
            _Configuration = new ConfigurationBuilder()
                            .SetBasePath(Path)
                            .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                            .Build();
        }

        /// <summary>
        /// 获取配置文件值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetVal(params string[] key)
        {
            if (key.Any())
            {
                return _Configuration[string.Join(":", key)];
            }
            else
                throw new ArgumentNullException($"不可传递参数为空.");
        }
    }
}

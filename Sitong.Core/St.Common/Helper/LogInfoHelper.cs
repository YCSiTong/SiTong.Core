using St.Extensions;
using System;
using System.IO;
using System.Threading;

namespace St.Common.Helper
{
    /// <summary>
    /// 文件读写
    /// </summary>
    public class LogInfoHelper
    {
        /// <summary>
        /// 读写锁
        /// </summary>
        private static ReaderWriterLockSlim _LockSlim = new ReaderWriterLockSlim();

        /// <summary>
        /// 程序文件地址
        /// </summary>
        private static string _RootPath = string.Empty;

        public LogInfoHelper(string rootPath)
        {
            rootPath.NotEmptyOrNull(nameof(rootPath));
            _RootPath = rootPath;
        }

        /// <summary>
        /// 年-月-日Log记录
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void WriteLog(string fileName, params string[] data)
        {
            try
            {
                var time = DateTime.Now;
                string path = Path.Combine(_RootPath, $"Logs\\{time:yyyy}\\{time:MM}\\{time:dd}");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string logFilePath = Path.Combine(path, $@"{fileName}.log");

                var now = DateTime.Now;
                string dataStr = "-----------------------------\r\n" +
                                $"{time:yyyy-MM-dd HH:mm:ss}\r\n" +
                                $"{string.Join("\r\n", data)}";

                _LockSlim.EnterWriteLock();// 开锁
                File.AppendAllText(logFilePath, dataStr);// 写入
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"{DateTime.Now} ==> 写入日志时错误 ==> {ex.Message}");
            }
            finally
            {
                _LockSlim.ExitWriteLock();// 立马释放锁
            }
        }

    }
}

using St.Exceptions;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace St.Common.Helper
{
    /// <summary>
    /// 操作文件
    /// </summary>
    public class FileHelper
    {
        private static ReaderWriterLockSlim _LockSlim = new ReaderWriterLockSlim();

        /// <summary>
        /// 写入内容到指定目录文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="data">文件内容</param>
        /// <returns></returns>
        public static async void WriteFileAsync(string path, string data)
        {
            try
            {
                _LockSlim.EnterWriteLock();
                await File.WriteAllText(path, data);
            }
            catch (Exception ex)
            {
                throw new BusinessException("当前写入文件异常 => " + ex.Message);
            }
            finally
            {
                _LockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// 支持模糊查询指定文件读取内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="liekName">模糊查询时所传名字</param>
        /// <param name="splitStr">多文件内容分割符(默认为*号)</param>
        /// <param name="IsLikeQuery">是否模糊查</param>
        /// <returns></returns>
        public static string ReadFileAsync(string path, string liekName, string splitStr = "*", bool IsLikeQuery = false)
        {
            string result = string.Empty;
            try
            {
                // 是否开启模糊文件搜索
                if (IsLikeQuery)
                {
                    var allFiles = new DirectoryInfo(path);// 不用Directory.GetFiles(path)是因为无法具体到文件名称进行模糊查询.
                    var files = allFiles.GetFiles(path).Where(file => file.Name.ToLower().Contains(liekName.ToLower())).ToArray();
                    if (files.Length > 0)
                    {
                        StringBuilder dataStr = new StringBuilder();
                        _LockSlim.EnterReadLock();
                        foreach (var item in files)
                        {
                            using StreamReader resultStream = new StreamReader(path);
                            dataStr.Append(resultStream.ReadToEnd());
                            dataStr.Append(splitStr);
                        }
                        result = dataStr.ToString();
                    }
                }
                else
                {
                    _LockSlim.EnterReadLock();
                    using StreamReader resultStream = new StreamReader(path);
                    result = resultStream.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("当前读文件异常 => " + ex.Message);
            }
            finally
            {
                _LockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void FileDelete(string path)
            => File.Delete(path);

        /// <summary>
        /// 拷贝文件至指定目录
        /// </summary>
        /// <param name="souceFilePath">原始文件</param>
        /// <param name="newFilePath">新文件路径</param>
        public static void FileCopy(string souceFilePath, string newFilePath)
            => File.Copy(souceFilePath, newFilePath);



    }
}

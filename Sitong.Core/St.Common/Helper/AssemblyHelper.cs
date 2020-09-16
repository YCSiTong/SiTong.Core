using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace St.Common.Helper
{
    /// <summary>
    /// 程序集封装
    /// </summary>
    public class AssemblyHelper
    {
        /// <summary>
        /// 获取项目所有的程序集
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAssemblys()
        {
            string[] filters =
           {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };
            List<Assembly> list = new List<Assembly>();
            var deps = DependencyContext.Default;
            //排除所有的系统程序集、Nuget下载包
            var libs = deps.CompileLibraries.Where(op => !op.Serviceable && op.Type != "package" && !filters.Any(op.Name.StartsWith));
            foreach (var lib in libs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }
            return list;
        }
    }
}

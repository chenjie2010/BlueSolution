//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AssemblyLocator.cs
// 描述: 程序集动态加载类
// 作者：ChenJie 
// 编写日期：2016-07-25
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 程序集动态加载类
    /// </summary>
    public sealed class AssemblyLocator
    {
        /// <summary>
        /// 通过程序集名称和类名获得对象
        /// </summary>
        /// <param name="dalPath">程序集名称</param>
        /// <param name="nameSpace">子命名空间</param>
        /// <param name="className">类名</param>
        /// <returns>反射创建的对象</returns>
        public static object CreateInstanceByAssembly(string assemblyPath, string nameSpace, string className)
        {
            string fullName = GetAssemblyPath(assemblyPath, nameSpace, className); 
            return Assembly.Load(assemblyPath).CreateInstance(fullName);
        }

        /// <summary>
        /// 获得类的完整的路径名称
        /// </summary>
        /// <param name="assemblyPath">程序集名称</param>
        /// <param name="nameSpace">子命名空间</param>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static string GetAssemblyPath(string assemblyPath, string nameSpace, string className)
        {
            StringBuilder fullName = new StringBuilder();
            fullName.Append(assemblyPath);
            fullName.Append(".");            
            if (!string.IsNullOrWhiteSpace(nameSpace))
            {
                fullName.Append(nameSpace);
                fullName.Append(".");
            }
            fullName.Append(className);

            return fullName.ToString();
        }
    }
}

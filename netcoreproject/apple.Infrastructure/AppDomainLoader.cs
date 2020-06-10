using System;
using System.Collections.Generic;
using System.Text;

namespace apple.Infrastructure
{
    public static class AppDomainLoader
    {
        /// <summary>
        /// 加载应用程序，获取作业实例
        /// </summary>
        /// <param name="assemblyPath">作业实例程序集的物理路径</param>
        /// <param name="classType">作业实例的完全限定名</param>
        /// <param name="appDomain"></param>
        /// <returns></returns>
        //public static BaseJob Load(string assemblyPath, string classType, out AppDomain appDomain)
        //{
        //    System.Runtime.AssemblyTargetedPatchBandAttribute

        //    //AppDomainSetup setup = new AppDomainSetup();
        //    //if (System.IO.File.Exists($"{assemblyPath}.config"))
        //    //{
        //    //    setup.ConfigurationFile = $"{assemblyPath}.config";
        //    //}
        //    //setup.ShadowCopyFiles = "true";
        //    //setup.ApplicationBase = System.IO.Path.GetDirectoryName(assemblyPath);
        //    //string appDomainName = System.IO.Path.GetFileName(assemblyPath);
        //    //if (string.IsNullOrWhiteSpace(appDomainName))
        //    //{
        //    //    throw new ArgumentNullException($"应用程序域名称为空,assemblyPath : {assemblyPath}");
        //    //}
        //    //appDomain = AppDomain.CreateDomain(appDomainName, null, setup);
        //    //AppDomain.MonitoringIsEnabled = true;
        //    //BaseJob obj = (BaseJob)appDomain.CreateInstanceFromAndUnwrap(assemblyPath, classType);
        //    //return obj;
        //}

        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="appDomain"></param>
        public static void UnLoad(AppDomain appDomain)
        {
            AppDomain.Unload(appDomain);
            appDomain = null;
        }
    }


    public abstract class BaseJob : MarshalByRefObject
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <returns>true:运行成功;false:运行失败</returns>
        public bool Run()
        {
            bool res = false;
            try
            {
                //LogService.WriteLog($"{DateTime.Now} : 开始执行");
                Execute();
                //LogService.WriteLog($"{DateTime.Now} : 执行结束\r\n");
                res = true;
            }
            catch (Exception ex)
            {
                //LogService.WriteLog(ex, GetType().Name);
            }
            return res;
        }


        /// <summary>
        /// 具体逻辑
        /// </summary>
        protected abstract void Execute();


        /// <summary>
        /// 将对象生存期更改为永久,因为CLR默认5分钟内没有通过代理发出调用,对象会实效,下次垃圾回收会释放它的内存.
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}

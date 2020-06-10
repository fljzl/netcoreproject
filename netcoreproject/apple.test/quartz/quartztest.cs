using apple.test.BaseTest;
using System;
using System.Collections.Generic;
using System.Text;
using apple.Infrastructure;
using apple.data.Quartz;
using System.Runtime.Loader;
using System.Reflection;
using System.IO;
using System.Threading;
using Quartz;
using System.Threading.Tasks;
using apple.model.quarzt;
using apple.core;

namespace apple.test.quartz
{
    public class quartztest : ITest
    {
        public void Log()
        {

            #region 测试dll

            //MangerLog log = new MangerLog();
            //log.Error("log" + DateTime.Now.ToLongDateString());
            //MangerQuartznet manger = new MangerQuartznet();
            //QuatzjobRepostory _quartzrepository = new QuatzjobRepostory();
            //var job = _quartzrepository.FindById(4);
            //manger.RunJob(job);

            #endregion



            #region 测试数据库
            //var manger = new QuatzjobRepostory();
            ////var detail = manger.FindById(4);
            //int totals = 0;
            //var list = manger.GetListPage("", 1, 10, ref totals);
            ////manger.UpdateBackgroundJobStatus(detail.JobId, 3);
            ////detail = manger.FindById(4);
            ////manger.UpdateBackgroundJobStatus(detail.JobId, DateTime.Now, default(DateTime));
            ///
            #endregion


            #region 失败了
            //List<LoadDll> list = new List<LoadDll>();
            //Console.WriteLine("开始加载DLL");
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskOne\bin\Debug\netstandard2.0\Demo.TaskOne.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskOne\bin\Debug\netstandard2.0\Demo.TaskOne.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskOne\bin\Debug\netstandard2.0\Demo.TaskOne.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskOne\bin\Debug\netstandard2.0\Demo.TaskOne.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskOne\bin\Debug\netstandard2.0\Demo.TaskOne.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //list.Add(Load(@"E:\项目文件\AssemblyLoadContextDemo\Demo.TaskTwo\bin\Debug\netstandard2.0\Demo.TaskTwo.dll"));
            //foreach (var item in list)
            //{
            //    item.StartTask();
            //}
            //Console.WriteLine("开启了任务!");
            //SpinWait.SpinUntil(() => false, 10 * 1000);
            //foreach (var item in list)
            //{
            //    var s = item.UnLoad();
            //    SpinWait.SpinUntil(() => false, 2 * 1000);
            //    Console.WriteLine($"任务卸载：{s}");
            //}
            //Console.WriteLine("任务测试完毕");

            #endregion


        }

        public void Log(string str)
        {
            throw new NotImplementedException();
        }

        #region MyRegion

        public static LoadDll Load(string filePath)
        {
            var load = new LoadDll();
            load.LoadFile(filePath);
            return load;
        }

        public class LoadDll
        {
            /// <summary>
            ///// 任务实体
            /// </summary>
            public IJob _task;
            public Thread _thread;
            /// <summary>
            /// 核心程序集加载
            /// </summary>
            public AssemblyLoadContext _AssemblyLoadContext { get; set; }
            /// <summary>
            /// 获取程序集
            /// </summary>
            public Assembly _Assembly { get; set; }
            /// <summary>
            /// 文件地址
            /// </summary>
            public string filepath = string.Empty;
            /// <summary>
            /// 指定位置的插件库集合
            /// </summary>
            AssemblyDependencyResolver resolver { get; set; }

            public bool LoadFile(string filepath)
            {
                this.filepath = filepath;
                try
                {
                    resolver = new AssemblyDependencyResolver(filepath);
                    _AssemblyLoadContext = new AssemblyLoadContext(Guid.NewGuid().ToString("N"), true);
                    _AssemblyLoadContext.Resolving += _AssemblyLoadContext_Resolving;

                    using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                    {
                        var _Assembly = _AssemblyLoadContext.LoadFromStream(fs);
                        var Modules = _Assembly.Modules;
                        foreach (var item in _Assembly.GetTypes())
                        {
                            if (item.GetInterface("IJob") != null)
                            {
                                _task = (IJob)Activator.CreateInstance(item);
                                break;
                            }
                        }
                        return true;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); };
                return false;
            }

            private Assembly _AssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
            {
                Console.WriteLine($"加载{arg2.Name}");
                var path = resolver.ResolveAssemblyToPath(arg2);
                if (!string.IsNullOrEmpty(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        return _AssemblyLoadContext.LoadFromStream(fs);
                    }
                }
                return null;
            }

            public bool StartTask()
            {
                bool RunState = false;
                try
                {
                    if (_task != null)
                    {
                        _thread = new Thread(new ThreadStart(_Run));
                        _thread.IsBackground = true;
                        _thread.Start();
                        RunState = true;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); };
                return RunState;
            }
            private void _Run()
            {
                try
                {
                    //_task.Execute();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); };
            }
            public bool UnLoad()
            {
                try
                {
                    _thread?.Interrupt();
                }
                catch (Exception)
                {
                }
                finally
                {
                    _thread = null;
                }
                _task = null;
                try
                {
                    _AssemblyLoadContext?.Unload();
                }
                catch (Exception)
                { }
                finally
                {
                    _AssemblyLoadContext = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                return true;
            }
        }



        #endregion


    }

    #region MyRegion

    public class PrintStr : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                int a = 0;
                while (true)
                {
                    Console.WriteLine($"PrintStr:{ a}");
                    a++;
                    Thread.Sleep(1 * 1000);
                }
            });
        }
    }

    public class PrintDate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
             {
                 while (true)
                 {
                     Console.WriteLine($"PrintDate:{ DateTime.Now.ToString()}");
                     Thread.Sleep(1 * 1000);
                 }
             });
        }
    }


    #endregion
}

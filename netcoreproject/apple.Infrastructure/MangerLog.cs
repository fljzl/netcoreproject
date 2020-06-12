//using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using log4net;
using log4net.Repository;

namespace apple.Infrastructure
{
    public class MangerLog
    {
        private ILog _logger;
        private ILoggerRepository loggerRepository { get; set; }

        private string _name = "corelog";
        public MangerLog(string name = "corelog")
        {
            _name = name;
            //loggerRepository = log4net.LogManager.CreateRepository(typeof(MangerLog).ToString());
            loggerRepository = log4net.LogManager.CreateRepository(
             Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            var file = new FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, file);
            //_logger = LogManager.GetLogger(typeof(MangerLog).ToString(), "loginfo");
            _logger = LogManager.GetLogger(loggerRepository.Name, name);
            //_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public MangerLog()
        {
            //loggerRepository = log4net.LogManager.CreateRepository(typeof(MangerLog).ToString());
            loggerRepository = log4net.LogManager.CreateRepository(
             Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            var file = new FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, file);
            //_logger = LogManager.GetLogger(typeof(MangerLog).ToString(), "loginfo");
            _logger = LogManager.GetLogger(loggerRepository.Name, _name);
            //_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Error(string msg, Exception ex = null)
        {
            if (ex != null)
            {
                _logger.Error(msg, ex);
            }
            else
            {
                _logger.Error(msg);
            }

        }
        public void Infor(string msg, Exception ex = null)
        {
            if (ex != null)
            {
                _logger.Info(msg, ex);
            }
            else
            {
                _logger.Info(msg);
            }
        }
    }
}

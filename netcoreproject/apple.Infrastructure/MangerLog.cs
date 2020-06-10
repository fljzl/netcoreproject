//using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using log4net.Repository;

namespace apple.Infrastructure
{
    public class MangerLog
    {
        private ILog _logger;
        private ILoggerRepository loggerRepository { get; set; }
        public MangerLog()
        {
            loggerRepository = log4net.LogManager.CreateRepository(typeof(MangerLog).ToString());
            var file = new FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, file);
            _logger = LogManager.GetLogger(typeof(MangerLog).ToString(), "loginfo");
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

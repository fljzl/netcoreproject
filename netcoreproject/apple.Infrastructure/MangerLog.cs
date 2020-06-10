using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.Infrastructure
{
    public class MangerLog
    {
        private Logger _logger;

        public MangerLog()
        {
            _logger = new LogFactory().GetCurrentClassLogger();
        }

        public void Error(string msg, Exception ex = null)
        {
            _logger.Error(ex, msg);
        }
        public void Infor(string msg, Exception ex = null)
        {
            _logger.Info(ex, msg);
        }
    }
}

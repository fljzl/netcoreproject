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
            var logs = string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { msg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
            _logger.Error(logs);
        }
        public void Infor(string msg, Exception ex = null)
        {
            var logs = string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { msg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
            _logger.Error(logs);
        }
    }
}

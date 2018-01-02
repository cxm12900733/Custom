using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Infrastructure
{
    public static class Log4NetExt
    {
        /// <summary>
        /// 返回日志对象
        /// </summary>
        public static log4net.ILog DebugFileLog()
        {
            return log4net.LogManager.GetLogger("DebugFileLog");
        }

        public static log4net.ILog DebugSQLLog()
        {
            return log4net.LogManager.GetLogger("DebugSQLLog");
        }
    }
}

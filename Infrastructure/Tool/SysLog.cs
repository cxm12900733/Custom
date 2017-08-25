using Infrastructure.ITool;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Infrastructure.Tool
{
    public class Log4Net: ISysLog
    {
        ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Warn(string message)
        {
            Log.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            Log.Debug(message, exception);
        }
    }
}

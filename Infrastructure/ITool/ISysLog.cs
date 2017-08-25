using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ITool
{
    public interface ISysLog
    {
        /// <summary>
        /// 至命错误
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
        void Fatal(string message, Exception exception);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        void Error(string message, Exception exception);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        void Warn(string message, Exception exception);

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        void Info(string message, Exception exception);

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        void Debug(string message, Exception exception);
    }
}

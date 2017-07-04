using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NLog.Config;

namespace Core.Common
{
    /// <summary>
    /// 日志封装
    /// </summary>
    public class Log
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static Log()
        {
            LogManager.Configuration = new XmlLoggingConfiguration(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\NLog.config");
        }

        /// <summary>
        /// 文件日志（文件）
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void Info(Exception ex,string msg)
        {
            logger.Info(ex,msg);
        }

        /// <summary>
        /// 程序错误日志（数据库）
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(Exception ex, string msg)
        {
            logger.Error(ex,msg);
        }
        /// <summary>
        /// 逻辑错误日志（数据库）
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }

        public static void Fatal(Exception ex, string msg)
        {
            logger.Fatal(ex, msg);
        }
        /// <summary>
        /// 警告日志（邮件）
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Warn(string msg)
        {
            try
            {
                logger.Warn(msg);
            }
            catch (Exception)
            {
                Log.Fatal("邮件发送失败！错误信息：" + msg);
            }
        }
        public static void Warn(Exception ex, string msg)
        {
            try
            {
                logger.Warn(ex, msg);
            }
            catch (Exception)
            {
                Log.Fatal("邮件发送失败！错误信息：" + msg);
            }
            
        }
    }
}

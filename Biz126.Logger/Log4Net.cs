using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Biz126.Logger
{
    public static class Log4Net
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(Log4Net));

        static void SetConfig()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="Message"></param>
        public static void LogInfo(string Message)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Info(Message);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void LogInfo(string Message, Exception ex)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Info(Message, ex);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="Message"></param>
        public static void ErrorInfo(string Message)
        {
            if (!log.IsErrorEnabled)
                SetConfig();
            log.Error(Message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            if (!log.IsInfoEnabled)
                SetConfig();
            log.Error("异常", ex);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void ErrorInfo(string Message, Exception ex)
        {
            if (!log.IsErrorEnabled)
                SetConfig();
            log.Error(Message, ex);
        }

        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="Message"></param>
        public static void DebugInfo(string Message)
        {
            if (!log.IsDebugEnabled)
                SetConfig();
            log.Debug(Message);
        }

        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void DebugInfo(string Message, Exception ex)
        {
            if (!log.IsDebugEnabled)
                SetConfig();
            log.Debug(Message, ex);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="Message"></param>
        public static void WarnInfo(string Message)
        {
            if (!log.IsWarnEnabled)
                SetConfig();
            log.Warn(Message);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void WarnInfo(string Message,Exception ex)
        {
            if (!log.IsWarnEnabled)
                SetConfig();
            log.Warn(Message,ex);
            
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="Message"></param>
        public static void FataInfo(string Message)
        {
            if (!log.IsFatalEnabled)
                SetConfig();
            log.Fatal(Message);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        public static void FataInfo(string Message,Exception ex)
        {
            if (!log.IsFatalEnabled)
                SetConfig();
            log.Fatal(Message, ex);
        }
    }
}

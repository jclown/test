using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;

namespace Lib
{
    public class Log
    {
        private static Mutex exceptionLogMutex = new Mutex();
        private static Mutex operationLogMutex = new Mutex();
        private static Mutex performanceLogMutex = new Mutex();
        private static Mutex securityLogMutex = new Mutex();

        static Log()
        {
            if (!System.IO.Directory.Exists(_logPath))
            {
                System.IO.Directory.CreateDirectory(_logPath);
            }
        }

        private static readonly string _logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log";

        public static string LogPath
        {
            get
            {
                return _logPath;
            }
        }

        /// <summary>
        /// 安全日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteSecurityLog(string message)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            WriteLog(securityLogMutex, message, "SecurityLog-" + dateString + ".txt");
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteOperationLog(string message)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            WriteLog(operationLogMutex, message, "OperationLog-" + dateString + ".txt");
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteExceptionLog(string message)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            WriteLog(exceptionLogMutex, message, "ExceptionLog-" + dateString + ".txt");
        }

        public static string GetExceptionLog(Exception ex, string flag = "")
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            string message = LogException(ex, dateString, flag);
            //WriteLog(exceptionLogMutex, message, "ExceptionLog-" + dateString + ".txt");
            return message;
        }

        private static string LogException(Exception ex, string dateString, string flag = "")
        {
            var message = $"{flag} Exception:{ex.Message}  <br> StackTrace:{ex.StackTrace}{Environment.NewLine}";
            
            if (ex.InnerException != null)
            {
                return message + LogException(ex.InnerException, dateString, flag);
            }
            return message;
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="message"></param>
        public static void WritePerformanceLog(string message)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            WriteLog(performanceLogMutex, message, "PerformanceLog-" + dateString + ".txt");
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="mut"></param>
        /// <param name="str">日志文本</param>
        /// <param name="logFilename">日志文件路径</param>
        private static void WriteLog(Mutex mut, string str, string logFilename)
        {
            var fileName = Lib.IOHelper.GetFileFullPath(LogPath + @"\" + logFilename);

            try
            {
                mut.WaitOne();
                if (!File.Exists(fileName))//如果文件不存在,则创建File.CreateText对象
                {
                    StreamWriter sw = File.CreateText(fileName);
                    sw.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + "--------" + str);
                    sw.Close();
                    sw.Dispose();
                }
                else //如果文件存在,则创建File.AppendText对象
                {
                    using (StreamWriter sr = new StreamWriter(fileName, true))
                    {
                        sr.WriteLine();
                        sr.WriteLine(DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff") + "--------" + str);
                        sr.Close();
                    }
                }
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="type">类型[ExceptionLog,DatabaseLog,OperationLog]</param>
        /// <param name="date">日期[2017-07-04]</param>
        /// <returns></returns>
        private static string GetLog(string type, string date)
        {
            string content = "";
            string strFileName = LogPath + @"\" + type + "-" + date + ".txt";
            if (File.Exists(strFileName))
            {
                StreamReader sr = new StreamReader(strFileName, Encoding.UTF8);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    content = content + line.ToString() + "<br>";
                }
                sr.Close();

            }
            else
            {
                content = "日志文件不存在";
            }
            return content;
        }

    }
}

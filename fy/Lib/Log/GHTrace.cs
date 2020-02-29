using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modobay.Log
{
      
 
    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public static class GHWeixinTrace
    {
        /// <summary>
        /// TraceListener
        /// </summary>
        private static TraceListener _traceListener = null;

        /// <summary>
        /// 统一日志锁名称
        /// </summary>
        const string LockName = "WeixinTraceLock";

        

        /// <summary>
        /// 打开日志开始记录
        /// </summary>
        internal static void Open()
        {
            Close();
            var logFile = Path.Combine("WeiXinLog",string.Format("WeixinTrace-{0}.log", DateTime.Now.ToString("yyyyMMdd"))); 
            logFile = Lib.IOHelper.GetFileFullPath(logFile).EnsureDirectory();
            System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
            _traceListener = new TextWriterTraceListener(logWriter);
            System.Diagnostics.Trace.Listeners.Add(_traceListener);
            System.Diagnostics.Trace.AutoFlush = true;

        }

        /// <summary>
        /// 关闭日志记录
        /// </summary>
        internal static void Close()
        {
            if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
            {
                _traceListener.Close();
                System.Diagnostics.Trace.Listeners.Remove(_traceListener);
            }
        }

        #region 私有方法

        /// <summary>
        /// 统一时间格式
        /// </summary>
        private static void TimeLog()
        {
            Log("[{0}]", DateTime.Now);
        }

        /// <summary>
        /// 当前线程记录
        /// </summary>
        private static void ThreadLog()
        {
            Log("[线程：{0}]", Thread.CurrentThread.GetHashCode());
        }


        /// <summary>
        /// 退回一次缩进
        /// </summary>
        private static void Unindent()
        {
            System.Diagnostics.Trace.Unindent();
        }

        /// <summary>
        /// 缩进一次
        /// </summary>
        private static void Indent()
        {
            System.Diagnostics.Trace.Indent();
        }

        /// <summary>
        /// 写入缓存到系统Trace
        /// </summary>
        private static void Flush()
        {
            System.Diagnostics.Trace.Flush();
        }

        /// <summary>
        /// 开始记录日志
        /// </summary>
        /// <param name="title"></param>
        private static void LogBegin(string title = null)
        {
            Open();
            Log("");
            if (title != null)
            {
                Log("[{0}]", title);
            }
            TimeLog();//记录时间
            ThreadLog();//记录线程
            Indent();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="messageFormat">日志内容格式</param>
        /// <param name="args">日志内容参数</param>
        public static void Log(string messageFormat, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(string.Format(messageFormat, args));
        }

        /// <summary>
        /// 结束日志记录
        /// </summary>
        private static void LogEnd()
        {
            Unindent();
            Flush();
            Close(); 
        }

        #endregion

        #region 日志记录

        /// <summary>
        /// 记录日志（建议使用SendXXLog()方法，以符合统一的记录规则）
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Log(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }


        /// <summary>
        /// 自定义日志
        /// </summary>
        /// <param name="typeName">日志类型</param>
        /// <param name="content">日志内容</param>
        public static void SendCustomLog(string typeName, string content)
        {
            
            LogBegin(string.Format("[[{0}]]", typeName));
            Log(content);
            LogEnd();
        }

        /// <summary>
        /// API请求日志
        /// </summary>
        /// <param name="url"></param>
        /// <param name="returnText"></param>
        public static void SendApiLog(string url, string returnText)
        {
           
            LogBegin("[[接口调用]]"); 
            Log("URL：{0}", url);
            Log("Result：\r\n{0}", returnText);
            LogEnd();
        }

        #endregion

        #region WeixinException 相关日志

        /// <summary>
        /// WeixinException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WeixinExceptionLog(Exception ex)
        {
            
            LogBegin("[[WeixinException]]");
            Log(ex.GetType().Name); 
            Log("Message：{0}", ex.Message);
            Log("StackTrace：{0}", ex.StackTrace);
            if (ex.InnerException != null)
            {
                Log("InnerException：{0}", ex.InnerException.Message);
                Log("InnerException.StackTrace：{0}", ex.InnerException.StackTrace);
            }

            
            LogEnd();

        }

       
        #endregion
    }

}

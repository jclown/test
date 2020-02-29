using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Lib
{
    public class StopwatchLog
    {

        public static bool IsEnablePerformanceLog { get; set; } = false;

        public static TResult StopwatchExce<T, TResult>(Func<T, TResult> func, T arg)
        {
            if (!IsEnablePerformanceLog) return func.Invoke(arg);

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            try
            {
                return func.Invoke(arg);
            }
            finally
            {
                stopwatch.Stop();
                Lib.Log.WritePerformanceLog($"{func.Target}.{func.Method.Name}:耗时{stopwatch.ElapsedMilliseconds}");
            }
        }

        public static TResult StopwatchExce<TResult>(Func<TResult> func)
        {
            if (!IsEnablePerformanceLog) return func.Invoke();

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            try
            {
                return func.Invoke();
            }
            finally
            {
                stopwatch.Stop();
                Lib.Log.WritePerformanceLog($"{func.Target}.{func.Method.Name}:耗时{stopwatch.ElapsedMilliseconds}");
            }
        }

        [ThreadStatic]
        private static Stopwatch _stopwatch = null;

        [ThreadStatic]
        public static List<LogItem> _stopwatchLogDetail = null;

        private static byte[] stopwatchLock = new byte[0] { };

        public static Stopwatch Stopwatch
        {
            get
            {
                if (_stopwatch == null)
                {
                    lock (stopwatchLock)
                    {
                        if (_stopwatch == null)
                        {
                            _stopwatchLogDetail = new List<LogItem>();
                            _stopwatch = new Stopwatch();
                            _stopwatch.Start();
                        }
                    }
                }
                return _stopwatch;
            }
        }

        public static void RecordElapsedMilliseconds(string flag)
        {
            if (!IsEnablePerformanceLog) return;
            try
            {
                StackFrame fr = new StackFrame(1, true);
                if (fr == null) return;
                MethodBase mb = fr.GetMethod();
                if (mb == null) return;
                var name = mb.Name;
                var logItem = new LogItem() { ActionName = name, Flag = flag, ElapsedMilliseconds = Stopwatch.ElapsedMilliseconds };
                _stopwatchLogDetail.Add(logItem);
                Stopwatch.Restart();
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog($"RecordElapsedMilliseconds : {ex.Message}  <br> StackTrace:{ex.StackTrace}");
            }
        }

        public static void WriteLog(string requestId = "")
        {
            if (!IsEnablePerformanceLog) return;
            const int ElapsedMilliseconds = -1;
            try
            {
                if (_stopwatchLogDetail == null || _stopwatchLogDetail.Count == 0 || !_stopwatchLogDetail.Exists(x => x.ElapsedMilliseconds >= ElapsedMilliseconds)) return;
                Stopwatch.Reset();
                Stopwatch.Stop();
                var threadFlag = Thread.CurrentThread.ManagedThreadId.ToString("00");
                var sb = new StringBuilder();
                sb.AppendLine($"Thread-{threadFlag} {Stopwatch.GetHashCode()}  RequestID: {requestId}");
                foreach (var item in _stopwatchLogDetail)
                {
                    if (item.ElapsedMilliseconds < ElapsedMilliseconds) continue;
                    sb.AppendLine($"{item.ActionName} {item.Flag}：{item.ElapsedMilliseconds}");
                }
                Lib.Log.WritePerformanceLog(sb.ToString());
                _stopwatchLogDetail.Clear();
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog($"WriteLog : {ex.Message}  <br> StackTrace:{ex.StackTrace}");
            }            
        }
    }

    public class LogItem
    {
        public string ActionName { get; set; }
        public string Flag { get; set; }
        public long ElapsedMilliseconds { get; set; }
        //public string RequestID { get; set; }
    }
}

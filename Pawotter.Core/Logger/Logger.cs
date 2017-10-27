using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Pawotter.Core.Logger
{
    public class Logger : ILogger
    {
        public static Logger Shared { get; } = new Logger(LogLevel.Info);
        readonly System.Object thisLock = new System.Object();

        public LogLevel LogLevel { get; set; }
        readonly string format;
        internal bool NeedsLog(LogLevel logLevel) => (int) logLevel >= (int) LogLevel;

        public Logger(LogLevel logLevel, string format = "[{0}] {3} ({1}:{2})")
        {
            LogLevel = logLevel;
            this.format = format;
        }

        public void Fatal(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Fatal, value, path, num);

        public void Error(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Error, value, path, num);

        public void Warn(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Warn, value, path, num);

        public void Info(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        public void Debug(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Debug, value, path, num);

        void Output(LogLevel logLevel, object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0)
        {
            if (!NeedsLog(logLevel)) return;
            var filename = Path.GetFileName(path);
            lock (thisLock)
            {
                Console.WriteLine(string.Format(format, logLevel, filename, num, value));
            }
        }
    }
}

using System.Runtime.CompilerServices;

namespace Pawotter.Core.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// fatal level log
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="value">Value.</param>
        /// <param name="path">Path.</param>
        /// <param name="num">Number.</param>
        void Fatal(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0);

        /// <summary>
        /// error level log
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="value">Value.</param>
        /// <param name="path">Path.</param>
        /// <param name="num">Number.</param>
        void Error(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0);

        /// <summary>
        /// warn level log
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="value">Value.</param>
        /// <param name="path">Path.</param>
        /// <param name="num">Number.</param>
        void Warn(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0);

        /// <summary>
        /// info level log
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="value">Value.</param>
        /// <param name="path">Path.</param>
        /// <param name="num">Number.</param>
        void Info(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0);

        /// <summary>
        /// debug level log
        /// </summary>
        /// <returns>The debug.</returns>
        /// <param name="value">Value.</param>
        /// <param name="path">Path.</param>
        /// <param name="num">Number.</param>
        void Debug(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0);
    }
}

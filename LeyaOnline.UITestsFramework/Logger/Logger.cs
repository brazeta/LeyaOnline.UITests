using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyaOnline.UITestsFramework.Logger
{
    public static class Logger
    {
        // A thread-safe list to store logs
        private static readonly StringBuilder logMessages = new StringBuilder();
        private static readonly object logLock = new object();

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        public static void LogDebug(string message)
        {
            WriteLog("DEBUG", message);
        }

        private static void WriteLog(string level, string message)
        {
            lock (logLock) // Ensures thread safety
            {
                var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

                // Add the log message to the list
                logMessages.AppendLine(logMessage);

                // We can output to Console or Debug window
                //Debug.WriteLine(logMessage);
                Console.WriteLine(logMessage);
            }
        }

        /// <summary>
        /// Print the entire log
        /// </summary>
        /// <returns></returns>
        public static string GetEntireLog()
        {
            return logMessages.ToString();
        }

        /// <summary>
        /// Get a trimmed version of the entire log
        /// </summary>
        /// <param name="MaximumCharacters"></param>
        /// <returns></returns>
        public static string GetTrimmedLog(int MaximumCharacters)
        {
            var entireLog = logMessages.ToString();
            return entireLog.Length > MaximumCharacters ? entireLog.Substring(0, MaximumCharacters) : entireLog;
        }

        /// <summary>
        /// Clear all Log messages
        /// </summary>
        public static void ClearLogs()
        {
            lock (logLock)
            {
                logMessages.Clear();
            }
        }
    }
}

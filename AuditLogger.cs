using System;
using System.IO;

namespace EduPermissionManager
{
    /// <summary>
    /// Complete audit trail of all permission escalation activities
    /// Logs all actions with timestamps
    /// </summary>
    public static class AuditLogger
    {
        private static string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EduPermissionManager");
        private static string logFile = Path.Combine(logDirectory, "audit.log");

        static AuditLogger()
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
        }

        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [INFO] {message}";
            
            Console.WriteLine(logEntry);
            WriteToFile(logEntry);
        }

        public static void Warn(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [WARN] {message}";
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logEntry);
            Console.ResetColor();
            
            WriteToFile(logEntry);
        }

        public static void Error(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [ERROR] {message}";
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logEntry);
            Console.ResetColor();
            
            WriteToFile(logEntry);
        }

        public static void Success(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [SUCCESS] {message}";
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(logEntry);
            Console.ResetColor();
            
            WriteToFile(logEntry);
        }

        private static void WriteToFile(string message)
        {
            try
            {
                File.AppendAllText(logFile, message + Environment.NewLine);
            }
            catch { }
        }

        public static string GetLogPath()
        {
            return logFile;
        }

        public static void ClearLogs()
        {
            try
            {
                if (File.Exists(logFile))
                    File.Delete(logFile);
                
                Log("Audit logs cleared");
            }
            catch (Exception ex)
            {
                Error($"Failed to clear logs: {ex.Message}");
            }
        }

        public static void ExportLogs(string exportPath)
        {
            try
            {
                if (File.Exists(logFile))
                {
                    File.Copy(logFile, exportPath, overwrite: true);
                    Log($"Logs exported to: {exportPath}");
                }
            }
            catch (Exception ex)
            {
                Error($"Failed to export logs: {ex.Message}");
            }
        }
    }
}

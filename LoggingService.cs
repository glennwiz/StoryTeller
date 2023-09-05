using System.Text.Json;

namespace Logging
{
    public class LoggerService
    {
        private string _logFolderPath; //TODO; make config parameter

        public LoggerService(string logFolderPath = null)
        {
            // Default to the current directory if no path is specified.
            _logFolderPath = logFolderPath ?? Directory.GetCurrentDirectory();
        }

        public void LogMessage(string message, LogLevel logLevel = LogLevel.Information)
        {
            bool running = true; //TODO; make config parameter
            if (running)
            {
                if(message is null or "")
                {
                    message = "null";
                }
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("Message cannot be null or whitespace.", nameof(message));
                }

                var logFileName = $"log{DateTime.Now:yyyyMMdd}.json";
                var logFilePath = Path.Combine(_logFolderPath, logFileName);

                List<LogEntry> logEntries;
                if (File.Exists(logFilePath))
                {
                    var existingContent = File.ReadAllText(logFilePath);
                    logEntries = JsonSerializer.Deserialize<List<LogEntry>>(existingContent);
                }
                else
                {
                    logEntries = new List<LogEntry>();
                }

                var logEntry = new LogEntry
                {
                    DateTime = DateTime.Now,
                    Message = message,
                    Caller = new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name,
                    LogLevel = logLevel
                };
                logEntries.Add(logEntry);

                var newLogContent = JsonSerializer.Serialize(logEntries, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(logFilePath, newLogContent);
            }
        }
        
        public enum LogLevel
        {
            Debug,
            Information,
            Warning,
            Error,
            Critical
        }

        public class LogEntry
        {
            public DateTime DateTime { get; set; }

            public string Message { get; set; }
    
            public string Caller { get; set; }
    
            public LogLevel LogLevel { get; set; }

            public string ThreadId { get; set; } 

            public string Exception { get; set; } 
    
            public string CorrelationId { get; set; }
        }
    }
}
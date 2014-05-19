#region Usings

using Microsoft.Practices.Prism.Logging;
using Serilog;
using Serilog.Context;

#endregion

namespace PuppyFramework.Services
{
    public class SerilogLogger : ILoggerFacade, Interfaces.ILogger
    {
        #region Fields

        private const string LOG_SOURCE = "LogSource";
        private readonly string _defaultLogSource;

        #endregion

        #region Constructors

        public SerilogLogger(string defaultLogSource, LoggerConfiguration loggerConfiguration = null)
        {
            _defaultLogSource = defaultLogSource;

            var config = loggerConfiguration
                         ?? new LoggerConfiguration().WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{LogSource:l}, {Level}] {Message}{NewLine}{Exception}").ReadAppSettings();
            Serilog.Log.Logger = config.Enrich.FromLogContext().CreateLogger();
            var message = loggerConfiguration == null ? "default built-in" : "user defined";
            Log("Serilog Logger has been configured with {Message:l} settings", Category.Info, _defaultLogSource, message);
        }

        #endregion

        #region Methods

        public void Log(string message, Category category, Priority priority)
        {
            Log(message, category, _defaultLogSource);
        }

        public void Log(string message, Category category, string logSource = null, params object[] propertyValues)
        {
            if (logSource == null)
            {
                logSource = _defaultLogSource;
            }

            using (LogContext.PushProperty(LOG_SOURCE, logSource))
            {
                switch (category)
                {
                    case Category.Debug:
                        Serilog.Log.Debug(message, propertyValues);
                        break;
                    case Category.Exception:
                        Serilog.Log.Error(message, propertyValues);
                        break;
                    case Category.Info:
                        Serilog.Log.Information(message, propertyValues);
                        break;
                    case Category.Warn:
                        Serilog.Log.Warning(message, propertyValues);
                        break;
                }
            }
        }

        #endregion
    }
}
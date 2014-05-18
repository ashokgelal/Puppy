#region Usings

using Microsoft.Practices.Prism.Logging;
using Serilog;

#endregion

namespace PuppyFramework.Services
{
    public class SerilogLogger : ILoggerFacade
    {
        #region Constructors

        public SerilogLogger(LoggerConfiguration loggerConfiguration = null)
        {
            var config = loggerConfiguration ?? new LoggerConfiguration().WriteTo.ColoredConsole();
            Serilog.Log.Logger = config.CreateLogger();
            var message = loggerConfiguration == null ? "default built-in" : "user defined";
            Serilog.Log.Information("Serilog Logger has been configured with {Message:1} settings", message);
        }

        #endregion

        #region Methods

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    Serilog.Log.Debug(message);
                    break;
                case Category.Exception:
                    Serilog.Log.Error(message);
                    break;
                case Category.Info:
                    Serilog.Log.Information(message);
                    break;
                case Category.Warn:
                    Serilog.Log.Warning(message);
                    break;
            }
        }

        #endregion
    }
}

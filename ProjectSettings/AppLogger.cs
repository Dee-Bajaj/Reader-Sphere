using System;
using Microsoft.Extensions.Logging;

namespace ProjectSettings
{
    public interface IAppLogger
    {
        void Log(string msg);
        void Log(string msg, Exception ex);
    }
    public class AppLogger : IAppLogger
    {
        private readonly ILogger<AppLogger> _logger;
        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }
        public void Log(string msg)
        {
            _logger.LogInformation(msg);
        }

        public void Log(string msg, Exception ex)
        {
            _logger.LogError(ex,msg,null);
        }
    }
}

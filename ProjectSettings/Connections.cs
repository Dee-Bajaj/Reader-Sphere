using Microsoft.Extensions.Configuration;
using System;

namespace ProjectSettings
{
    public interface IConnections
    {
        string DefaultConnection { get; set; }
    }
    public class Connections : IConnections
    {
        public string DefaultConnection { get; set; }
        private readonly IConfiguration _config;
        private readonly IAppLogger _appLogger;
        public Connections(IConfiguration config, IAppLogger appLogger)
        {
            _config = config;
            _appLogger = appLogger;
            SetupConnections();
        }

        private void SetupConnections()
        {
            try
            {
                DefaultConnection = _config["ConnectionStrings:MainDBConnectionString"];
            }
            catch(Exception ex)
            {
                _appLogger.Log("Exception caught at Connections.SetupConnections", ex);
            }
        }
    }
}

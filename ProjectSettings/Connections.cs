using Microsoft.Extensions.Configuration;

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
        public Connections(IConfiguration config)
        {
            _config = config;
            SetupConfig();
        }

        private void SetupConfig()
        {
            DefaultConnection = _config["ConnectionStrings:MainDBConnectionString"];
        }
    }
}

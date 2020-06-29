using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProjectSettings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBHealthCheckProvider : IHealthCheck
    {
        private readonly ReaderSphereContext _readerSphereContext;
        private readonly IAppLogger _appLogger;
        public DBHealthCheckProvider(ReaderSphereContext readerSphereContext, IAppLogger appLogger)
        {
            _readerSphereContext = readerSphereContext;
            _appLogger = appLogger;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_readerSphereContext.Database.CanConnect())
                    return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch(Exception ex)
            {
                _appLogger.Log("Exception caught at DBHealthCheckProvider.CheckHealthAsync", ex);
            }
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}

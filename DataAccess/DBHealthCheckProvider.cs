using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBHealthCheckProvider : IHealthCheck
    {
        private readonly ReaderSphereContext _readerSphereContext;
        public DBHealthCheckProvider(ReaderSphereContext readerSphereContext)
        {
            _readerSphereContext = readerSphereContext;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_readerSphereContext.Database.CanConnect())
                    return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch
            {

            }
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}

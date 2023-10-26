using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.API.Controllers
{
    public class GamePersistenceHealthCheck : IHealthCheck
    {
        private string _storagePath = @"..\GameOfLifeAPI\JSON"; // Reemplaza con la ruta real

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Directory.Exists(_storagePath))
                {
                    string[] files = Directory.GetFiles(_storagePath);
                    if (files.Length > 0)
                    {
                        return Task.FromResult(HealthCheckResult.Healthy("Game persistence is healthy."));
                    }
                }

                return Task.FromResult(HealthCheckResult.Unhealthy("Game persistence is not healthy."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Game persistence check failed: {ex.Message}"));
            }
        }
    }
}

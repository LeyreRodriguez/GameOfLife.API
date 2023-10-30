using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.API.Controllers
{
    public class GamePersistenceHealthCheck : IHealthCheck
    {
        private string _storagePath = @"JSON"; // Reemplaza con la ruta real

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Directory.Exists(_storagePath))
                {
                    // Intenta acceder al directorio para verificar los permisos de escritura
                    if (HasWritePermissions(_storagePath))
                    {
                        return Task.FromResult(HealthCheckResult.Healthy("Game persistence is healthy."));
                    }

                    return Task.FromResult(HealthCheckResult.Unhealthy("Insufficient write permissions in the storage path."));
                }

                return Task.FromResult(HealthCheckResult.Unhealthy("Game persistence is not healthy."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Game persistence check failed: {ex.Message}"));
            }
        }

        private bool HasWritePermissions(string directoryPath)
        {
            try
            {
                using (FileStream fs = File.Create(Path.Combine(directoryPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                {
                    // Si podemos crear un archivo temporal en la carpeta, tenemos permisos de escritura
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

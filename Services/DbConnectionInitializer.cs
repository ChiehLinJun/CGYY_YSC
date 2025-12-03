using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using CGYY_YSC.Entity;
using System.Runtime.Versioning;

namespace CGYY_YSC.Services
{
    /// <summary>
    /// Uses IOptionsMonitor to initialize and refresh DB connection strings
    /// whenever ConfigEntity (bound from config.json) changes.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DbConnectionInitializer : IHostedService
    {
        private readonly IOptionsMonitor<ConfigEntity> _configMonitor;

        public DbConnectionInitializer(IOptionsMonitor<ConfigEntity> configMonitor)
        {
            _configMonitor = configMonitor;

            // Initial load
            UpdateConnections(_configMonitor.CurrentValue);

            // Subscribe to changes (config.json reloadOnChange)
            _configMonitor.OnChange(UpdateConnections);
        }

        private void UpdateConnections(ConfigEntity config)
        {
            if (config == null)
            {
                Log.DBWARNLog("[CONFIG][DbConnectionInitializer] ConfigEntity is null, skip updating DB connections.");
                return;
            }

            Log.DBWARNLog("[CONFIG][DbConnectionInitializer] Updating DB connection strings from config.json.");

            // Oracle and Access BaseDB use static Initialize methods
            Model.OracleDB.BaseDB.Initialize(config);
            Model.ACCESS.BaseDB.Initialize(config);

            Log.DBWARNLog("[CONFIG][DbConnectionInitializer] DB connection strings updated successfully.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Nothing else to do; work is done via constructor and OnChange callback
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

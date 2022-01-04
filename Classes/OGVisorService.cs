using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using OG_Visor_Service.Helpers;

namespace OG_Visor_Service.Classes
{
    public sealed class OGVisorService : BackgroundService
    {
        private readonly HiveManager _hiveManager;
        private readonly Accountant _accountant;
        private readonly ILogger<OGVisorService> _logger;
        
        public OGVisorService(
            HiveManager hiveManager,
            Accountant accountant,
            ILogger<OGVisorService> logger) =>
            (_hiveManager, _accountant, _logger) = (hiveManager,accountant, logger);
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //string joke = await _hiveManager.UpdateData(stoppingToken);
                _logger.LogWarning(await _hiveManager.UpdateData(stoppingToken));

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                
            }
        }
        public void UseLogger(string log)
        {
            _logger.LogWarning(log);
        }
    }
}
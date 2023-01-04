using Microsoft.Extensions.DependencyInjection;
using WindowsService;
using static System.Formats.Asn1.AsnWriter;

namespace RandomWordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
       
        private readonly IServiceScopeFactory _service;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        int executionCount = 0;
        

        public Worker(ILogger<Worker> logger , IServiceScopeFactory service , IHostApplicationLifetime hostApplicationLifetime )
        {
            _logger = logger;
            _service = service;
            _hostApplicationLifetime = hostApplicationLifetime;
            
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                using (var scope = _service.CreateScope())
                {
                    var textService = scope.ServiceProvider.GetRequiredService<ITextService>();
                            
                    textService.CreateWord();

                }
            
                executionCount++;
                _logger.LogInformation(executionCount.ToString());

                await Task.Delay(1000, stoppingToken);
               
                if (executionCount == 10) {

                    await base.StopAsync(stoppingToken);
                
                
                }


                
            }
        }   
    }
}
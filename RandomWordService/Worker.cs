using Microsoft.Extensions.DependencyInjection;
using WindowsService;

namespace RandomWordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
       
        private readonly IServiceScopeFactory _service;
        public Worker(ILogger<Worker> logger , IServiceScopeFactory service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                using (var scope = _service.CreateScope())
                {
                    var textService = scope.ServiceProvider.GetRequiredService<ITextService>();
                            
                    await textService.CreateWord();
                    
                }

              
                await Task.Delay(3000, stoppingToken);


            }
        }
    }
}
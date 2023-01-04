using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using RandomWordService;
using static System.Formats.Asn1.AsnWriter;

namespace RandomWordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _service;
        private readonly IHostApplicationLifetime _hostApplicationLifetime; //servisi kendi kendine kapatmak için kullanýlabilir?.
        int executionCount = 0;
        

        public Worker(ILogger<Worker> logger , IServiceScopeFactory service , IHostApplicationLifetime hostApplicationLifetime )
        {
            _logger = logger;
            _service = service;
            _hostApplicationLifetime = hostApplicationLifetime;

            
        }


        public override async Task StartAsync(CancellationToken stoppingToken) 
        {
            CancellationTokenSource _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);


            _logger.LogInformation("Service starting");

            await base.StartAsync(_stoppingCts.Token);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CancellationTokenSource _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            


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

                await Task.Delay(500, _stoppingCts.Token);

                if (executionCount == 10)
                {
                    await base.StopAsync(_stoppingCts.Token);
                    _hostApplicationLifetime.StopApplication();

                }
            }

        }


        //public override async Task StopAsync(CancellationToken stoppingToken)
        //{
        //    var stopWatch = Stopwatch.StartNew();
        //    _logger.LogInformation("ReaderWorker stopped at: {time}", DateTimeOffset.Now);

        //    if (executionCount == 10)
        //    {
        //        await base.StopAsync(stoppingToken);

        //    }
        //    await base.StopAsync(stoppingToken);
        //    _logger.LogInformation("ReaderWorker took {ms} ms to stop.", stopWatch.ElapsedMilliseconds);
        //}
    }
}
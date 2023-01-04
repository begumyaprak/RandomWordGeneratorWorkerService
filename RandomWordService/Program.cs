using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RandomWordService;
using WindowsService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TextContext>(

        options => options.UseSqlServer("name=ConnectionStrings:DefaultConn"));


        services.AddScoped<ITextService,TextService>(); //manuel scopedan al�yor.
        services.AddHostedService<Worker>();
        
        
        
    })
    .Build();

await host.RunAsync();

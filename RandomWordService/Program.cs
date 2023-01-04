using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RandomWordService;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<TextContext>(

        options => options.UseSqlServer("name=ConnectionStrings:DefaultConn"));


        services.AddScoped<ITextService,TextService>(); //manuel scopedan alýyor.
        services.AddHostedService<Worker>();
        
        
        
    })
    .Build();

await host.RunAsync();

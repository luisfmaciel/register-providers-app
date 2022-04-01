using Domain;
using Infrastructure;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IProfessionalRepository, ProfessionalOnFileRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

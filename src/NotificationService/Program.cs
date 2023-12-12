using MassTransit;
using NotificationService.Consumers;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt", false));

        x.UsingRabbitMq((context, config) =>
        {
            config.UseMessageRetry(retry => 
            {
                retry.Handle<RabbitMqConnectionException>(); 
                retry.Interval(5, TimeSpan.FromSeconds(10));
            });
        
            config.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            config.ConfigureEndpoints(context);
        });
    });

    builder.Services.AddSignalR();

var app = builder.Build();

    app.MapHub<NotificationHub>("/notifications");

    app.Run();

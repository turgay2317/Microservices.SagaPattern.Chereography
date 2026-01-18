using MassTransit;
using Microservices.SagaPattern.Chereography.PaymentAPI.Consumers;
using Microservices.SagaPattern.Chereography.Shared;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(configuration =>
{
    configuration.AddConsumer<InventoryReservedEventConsumer>();
    configuration.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);
        _configurator.ReceiveEndpoint(RabbitMQSettings.Payment_InventoryReservedEventQueue, e =>
            e.ConfigureConsumer<InventoryReservedEventConsumer>(context)
        );
    });
});
var app = builder.Build();

app.Run();

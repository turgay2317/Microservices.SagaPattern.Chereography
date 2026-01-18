using MassTransit;
using Microservices.SagaPattern.Chereography.OrderAPI.Consumers;
using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Entities;
using Microservices.SagaPattern.Chereography.OrderAPI.Dtos;
using Microservices.SagaPattern.Chereography.OrderAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared;
using Microservices.SagaPattern.Chereography.Shared.Events;
using Microservices.SagaPattern.Chereography.Shared.Messages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Masstransit
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<PaymentCompletedEventConsumer>();
    configurator.AddConsumer<PaymentFailedEventConsumer>();
    configurator.AddConsumer<InventoryNotReservedEventConsumer>();
    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);
        _configurator.ReceiveEndpoint(RabbitMQSettings.Order_PaymentCompletedEventQueue, e =>
            e.ConfigureConsumer<PaymentCompletedEventConsumer>(context)
        );
        _configurator.ReceiveEndpoint(RabbitMQSettings.Order_PaymentFailedEventQueue, e =>
            e.ConfigureConsumer<PaymentFailedEventConsumer>(context)
        );
        _configurator.ReceiveEndpoint(RabbitMQSettings.Order_InventoryNotReservedEventQueue, e =>
            e.ConfigureConsumer<InventoryNotReservedEventConsumer>(context)
        );
    });
});

// SQL Server
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/create-order", async (CreateOrderDto request, OrderDbContext context, IPublishEndpoint publisher) =>
{
    Order order = new Order
    {
        CustomerId = request.CustomerId,
        Items = request.Items.Select(x => new OrderItem
        {
            ProductId = x.ProductId,
            Amount = x.Amount,
            Total = x.Price * x.Amount
        }).ToList(),
        Total = request.Items.Sum(x => x.Amount * x.Price)
    };

    await context.Orders.AddAsync(order);
    await context.SaveChangesAsync();

    OrderCreatedEvent e = new OrderCreatedEvent
    {
        CustomerId = order.CustomerId,
        OrderId = order.Id,
        TotalPrice = order.Total,
        Items = order.Items.Select(x => new OrderItemMessage
        {
            Amount = x.Amount,
            ProductId = x.ProductId,
            Total = x.Total,
        }).ToList()
    };

    await publisher.Publish(e);
    Console.WriteLine("Order is created: OrderCreatedEvent");
});

app.Run();

using MassTransit;
using Microservices.SagaPattern.Chereography.InventoryAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared;
using Microservices.SagaPattern.Chereography.Shared.Events;
using Microsoft.EntityFrameworkCore;

namespace Microservices.SagaPattern.Chereography.InventoryAPI.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly InventoryDbContext dbContext;
        private readonly ISendEndpointProvider provider;
        private readonly IPublishEndpoint publisher;

        public OrderCreatedEventConsumer(InventoryDbContext dbContext, ISendEndpointProvider provider, IPublishEndpoint publisher)
        {
            this.dbContext = dbContext;
            this.provider = provider;
            this.publisher = publisher;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var e = context.Message;
            var orderRequests = context.Message.Items.ToDictionary(k => k.ProductId, v => v.Amount);

            var inventory = await dbContext.Inventories
                .Where(x => orderRequests.Keys.Contains(x.ProductId))
                .ToListAsync();

            if (orderRequests.Count() != inventory.Count())
            {
                await PublishInventoryNotReservedEvent(e, $"Some of the products don't exist");
                return;
            }

            foreach (var product in inventory)
            {
                var requestedAmount = orderRequests.GetValueOrDefault(product.ProductId);

                if (product.Count < requestedAmount)
                {
                    await PublishInventoryNotReservedEvent(e, $"Product {product.ProductId} doesn't have enough in the stock");
                    return;
                }
                product.Count -= requestedAmount;
            }

            await dbContext.SaveChangesAsync();
            await SendInventoryReservedEvent(e);
        }

        private async Task PublishInventoryNotReservedEvent(OrderCreatedEvent e, string message = "")
        {
            InventoryNotReservedEvent @event = new()
            {
                CustomerId = e.CustomerId,
                OrderId = e.OrderId,
                Message = message
            };

            await publisher.Publish(@event);
            Console.WriteLine("Order is created but inventory is not reserved: InventoryNotReservedEvent");
        }

        private async Task SendInventoryReservedEvent(OrderCreatedEvent e)
        {
            Uri url = new Uri($"queue:{RabbitMQSettings.Payment_InventoryReservedEventQueue}");
            var endpoint = await provider.GetSendEndpoint(url);

            InventoryReservedEvent @event = new()
            {
                CustomerId = e.CustomerId,
                OrderId = e.OrderId,
                Items = e.Items,
                Total = e.TotalPrice
            };

            await endpoint.Send(@event);
            Console.WriteLine("Order is created and inventory is reserved: InventoryReservedEvent");
        }
    }
}

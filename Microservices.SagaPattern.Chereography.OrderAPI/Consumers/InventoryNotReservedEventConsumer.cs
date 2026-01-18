using MassTransit;
using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Enums;
using Microservices.SagaPattern.Chereography.OrderAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared.Events;

namespace Microservices.SagaPattern.Chereography.OrderAPI.Consumers
{
    public class InventoryNotReservedEventConsumer(OrderDbContext dbContext) : IConsumer<InventoryNotReservedEvent>
    {
        public async Task Consume(ConsumeContext<InventoryNotReservedEvent> context)
        {
            var e = context.Message;
            var order = await dbContext.Orders.FindAsync(e.OrderId);
            if (order == null)
                throw new NullReferenceException("Order not found");

            order.Status = OrderStatus.Failed;
            await dbContext.SaveChangesAsync();
        }
    }
}

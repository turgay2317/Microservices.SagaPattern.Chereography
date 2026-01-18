using MassTransit;
using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Enums;
using Microservices.SagaPattern.Chereography.OrderAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared.Events;

namespace Microservices.SagaPattern.Chereography.OrderAPI.Consumers
{
    public class PaymentFailedEventConsumer(OrderDbContext dbContext) : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var e = context.Message;
            var order = await dbContext.Orders.FindAsync(e.OrderId);
            if (order == null)
                throw new NullReferenceException("Order not found");

            order.Status = OrderStatus.Failed;
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Payment is failed and OrderStatus=Failed. OK.");
        }
    }
}

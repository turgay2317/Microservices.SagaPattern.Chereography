using MassTransit;
using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Enums;
using Microservices.SagaPattern.Chereography.OrderAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared.Events;

namespace Microservices.SagaPattern.Chereography.OrderAPI.Consumers
{
    public class PaymentCompletedEventConsumer(OrderDbContext dbContext) : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var e = context.Message;
            var order = await dbContext.Orders.FindAsync(e.OrderId);
            if (order == null)
                throw new NullReferenceException("Order not found");

            order.Status = OrderStatus.Completed;
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Payment is completed and OrderStatus=Completed. OK.");
        }
    }
}

using MassTransit;
using Microservices.SagaPattern.Chereography.PaymentAPI.Enums;
using Microservices.SagaPattern.Chereography.Shared.Events;

namespace Microservices.SagaPattern.Chereography.PaymentAPI.Consumers
{
    public class InventoryReservedEventConsumer(IPublishEndpoint publisher) : IConsumer<InventoryReservedEvent>
    {
        public async Task Consume(ConsumeContext<InventoryReservedEvent> context)
        {
            var e = context.Message;
            var values = Enum.GetValues<PaymentMethod>();
            var paymentMethod = values[Random.Shared.Next(values.Length)];
            int isPaymentSucceed = new Random().Next(0, 1);

            if (isPaymentSucceed == 1)
            {
                await PublishPaymentCompletedEvent(e, paymentMethod);
            }
            else
            {
                await PublishPaymentFailedEvent(e, paymentMethod, "An unexpected error occured");
            }
        }

        private async Task PublishPaymentCompletedEvent(InventoryReservedEvent e, PaymentMethod method)
        {
            var @event = new PaymentCompletedEvent
            {
                CustomerId = e.CustomerId,
                OrderId = e.OrderId,
                PaymentMethod = (int)method
            };
            await publisher.Publish(@event);
            Console.WriteLine("Inventory is reserved and payment is completed: PaymentCompletedEvent");
        }

        private async Task PublishPaymentFailedEvent(InventoryReservedEvent e, PaymentMethod method, string message)
        {
            var @event = new PaymentFailedEvent
            {
                CustomerId = e.CustomerId,
                OrderId = e.OrderId,
                PaymentMethod = (int)method,
                Items = e.Items,
                Message = message
            };
            await publisher.Publish(@event);
            Console.WriteLine("Inventory is reserved but payment is failed: PaymentFailedEvent");

        }
    }
}

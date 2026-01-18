using Microservices.SagaPattern.Chereography.Shared.Messages;

namespace Microservices.SagaPattern.Chereography.Shared.Events
{
    public class OrderCreatedEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> Items { get; set; } = new();
    }
}

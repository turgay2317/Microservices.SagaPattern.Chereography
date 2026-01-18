using Microservices.SagaPattern.Chereography.Shared.Messages;

namespace Microservices.SagaPattern.Chereography.Shared.Events
{
    public class PaymentFailedEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public int PaymentMethod { get; set; }
        public List<OrderItemMessage> Items { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
}

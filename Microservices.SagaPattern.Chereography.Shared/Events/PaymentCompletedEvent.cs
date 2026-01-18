namespace Microservices.SagaPattern.Chereography.Shared.Events
{
    public class PaymentCompletedEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public int PaymentMethod { get; set; }
    }
}

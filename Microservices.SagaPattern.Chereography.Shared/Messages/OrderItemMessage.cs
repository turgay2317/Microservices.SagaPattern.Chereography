namespace Microservices.SagaPattern.Chereography.Shared.Messages
{
    public class OrderItemMessage
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
    }
}

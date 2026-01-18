namespace Microservices.SagaPattern.Chereography.OrderAPI.Domain.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
    }
}

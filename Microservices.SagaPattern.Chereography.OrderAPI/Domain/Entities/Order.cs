using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Enums;

namespace Microservices.SagaPattern.Chereography.OrderAPI.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

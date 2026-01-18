namespace Microservices.SagaPattern.Chereography.OrderAPI.Dtos
{
    public class CreateOrderItemDto
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}

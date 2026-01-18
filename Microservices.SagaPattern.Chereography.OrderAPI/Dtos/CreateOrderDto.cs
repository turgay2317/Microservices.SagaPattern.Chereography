namespace Microservices.SagaPattern.Chereography.OrderAPI.Dtos
{
    public class CreateOrderDto
    {
        public long CustomerId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}

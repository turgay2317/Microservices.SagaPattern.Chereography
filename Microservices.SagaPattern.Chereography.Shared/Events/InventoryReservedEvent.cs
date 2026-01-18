namespace Microservices.SagaPattern.Chereography.Shared.Events
{
    public class InventoryReservedEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public decimal Total { get; set; }
    }
}

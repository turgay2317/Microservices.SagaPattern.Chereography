namespace Microservices.SagaPattern.Chereography.Shared.Events
{
    public class InventoryNotReservedEvent
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

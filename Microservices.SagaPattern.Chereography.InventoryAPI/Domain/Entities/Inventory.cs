namespace Microservices.SagaPattern.Chereography.InventoryAPI.Domain.Entities
{
    public class Inventory
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}

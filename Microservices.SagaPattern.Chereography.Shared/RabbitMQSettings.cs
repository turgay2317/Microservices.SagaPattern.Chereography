namespace Microservices.SagaPattern.Chereography.Shared
{
    public static class RabbitMQSettings
    {
        public const string Inventory_OrderCreatedEventQueue = "stock-order-created-event-queue";
        public const string Payment_InventoryReservedEvent = "payment-inventory-reserved-event-queue";
        public const string Payment_InventoryNotReservedEvent = "payment-inventory-not-reserved-event-queue";
    }
}

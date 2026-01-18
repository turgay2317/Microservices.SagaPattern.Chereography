namespace Microservices.SagaPattern.Chereography.Shared
{
    public static class RabbitMQSettings
    {
        public const string Inventory_OrderCreatedEventQueue = "stock-order-created-event-queue";
        public const string Payment_InventoryReservedEventQueue = "payment-inventory-reserved-event-queue";
        public const string Payment_InventoryNotReservedEventQueue = "payment-inventory-not-reserved-event-queue";
        public const string Order_PaymentCompletedEventQueue = "order-payment-completed-event-queue";
        public const string Order_PaymentFailedEventQueue = "order-payment-failed-queue";
        public const string Inventory_PaymentFailedEventQueue = "inventory-payment-failed-event-queue";
        public const string Order_InventoryNotReservedEventQueue = "order-inventory-not-reserved-event-queue";
    }
}

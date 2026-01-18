using MassTransit;
using Microservices.SagaPattern.Chereography.InventoryAPI.Persistence;
using Microservices.SagaPattern.Chereography.Shared.Events;
using Microsoft.EntityFrameworkCore;

namespace Microservices.SagaPattern.Chereography.InventoryAPI.Consumers
{
    public class PaymentFailedEventConsumer(InventoryDbContext dbContext) : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var e = context.Message;
            var failedDictionary = e.Items.ToDictionary(k => k.ProductId, v => v.Amount);
            var inventories = await dbContext.Inventories
                .Where(x => e.Items.Select(y => y.ProductId).Contains(x.ProductId))
                .ToListAsync();

            foreach (var inv in inventories)
            {
                inv.Count += failedDictionary.GetValueOrDefault(inv.ProductId);
            }

            await dbContext.SaveChangesAsync();
            Console.WriteLine("Oops payment is failed, I re-add inv back.");
        }
    }
}

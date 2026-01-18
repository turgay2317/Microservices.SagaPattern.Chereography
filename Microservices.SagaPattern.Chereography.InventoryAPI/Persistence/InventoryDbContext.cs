using Microservices.SagaPattern.Chereography.InventoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.SagaPattern.Chereography.InventoryAPI.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories { get; set; }
    }
}

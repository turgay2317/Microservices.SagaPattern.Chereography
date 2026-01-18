using Microservices.SagaPattern.Chereography.OrderAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.SagaPattern.Chereography.OrderAPI.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}

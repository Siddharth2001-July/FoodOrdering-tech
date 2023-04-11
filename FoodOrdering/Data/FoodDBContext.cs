using FoodOrdering.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FoodOrdering.Data
{
    public class FoodDBContext : IdentityDbContext
    {
            public FoodDBContext(DbContextOptions<FoodDBContext> options) : base(options)
        { }

        public DbSet<OrderModel> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
            }
    }
}

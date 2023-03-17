using AtSepete.Entities.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.DataAccess.Context
{
    
    
        public class AtSepeteDbContext : DbContext
        {
            public AtSepeteDbContext(DbContextOptions<AtSepeteDbContext> options) : base(options)
            {

            }
            public DbSet<Category> Categories { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Market> Markets { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetail> OrderDetails { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductMarket> ProductMarkets { get; set; }
        }
    
}

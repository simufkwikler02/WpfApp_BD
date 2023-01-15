using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WpfApp_BD;

namespace WpfApp_BD
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base(nameOrConnectionString: "Default") { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Empl { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Inventory> Invent { get; set; }
        public DbSet<Responsibilites> Respons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCompound> OrdComp { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Database
{
    public class ApiDbContext : DbContext
    {
        private static IConfiguration _configuration;

        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                optionsBuilder.UseSqlServer("Server=DESKTOP-8MLAUTE\\SQLEXPRESS;Database=stefanini_orders;User Id=sa;Password=sa123;TrustServerCertificate=True;");
            }
        }

        public DbSet<OrderModel> Order { get; set; }
        public DbSet<ItemOrderModel> ItemOrder { get; set; }
        public DbSet<ProductModel> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>().ToTable("order");
            modelBuilder.Entity<ItemOrderModel>().ToTable("item_order");
            modelBuilder.Entity<ProductModel>().ToTable("product");
            
            modelBuilder.Entity<OrderModel>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ItemOrderModel>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductModel>().Property(p => p.Id).ValueGeneratedOnAdd();
        } 
    }
}

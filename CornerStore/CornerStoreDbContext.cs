using CornerStore.Models;
using Microsoft.EntityFrameworkCore;

public class CornerStoreDbContext : DbContext
{
    // First, declare all DbSet properties
    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    // Constructor - you already have this
    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context)
        : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.ProductId, op.OrderId });

        // Following the same pattern as your library project
        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category[]
                {
                    new Category { Id = 1, CategoryName = "Beverages" },
                    new Category { Id = 2, CategoryName = "Snacks" },
                    new Category { Id = 3, CategoryName = "Dairy" },
                    new Category { Id = 4, CategoryName = "Produce" },
                }
            );

        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product[]
                {
                    new Product
                    {
                        Id = 1,
                        ProductName = "Cola",
                        Price = 2.49M,
                        Brand = "RefreshCo",
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "Potato Chips",
                        Price = 3.99M,
                        Brand = "CrunchCo",
                        CategoryId = 2,
                    },
                    new Product
                    {
                        Id = 3,
                        ProductName = "Milk",
                        Price = 4.99M,
                        Brand = "FreshFarms",
                        CategoryId = 3,
                    },
                    new Product
                    {
                        Id = 4,
                        ProductName = "Bananas",
                        Price = 0.99M,
                        Brand = "Nature's Best",
                        CategoryId = 4,
                    },
                    new Product
                    {
                        Id = 5,
                        ProductName = "Energy Drink",
                        Price = 3.49M,
                        Brand = "PowerUp",
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Id = 6,
                        ProductName = "Chocolate Bar",
                        Price = 1.99M,
                        Brand = "SweetTreats",
                        CategoryId = 2,
                    },
                }
            );

        modelBuilder
            .Entity<Cashier>()
            .HasData(
                new Cashier[]
                {
                    new Cashier
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Smith",
                    },
                    new Cashier
                    {
                        Id = 2,
                        FirstName = "Sarah",
                        LastName = "Johnson",
                    },
                    new Cashier
                    {
                        Id = 3,
                        FirstName = "Michael",
                        LastName = "Brown",
                    },
                }
            );

        modelBuilder
            .Entity<Order>()
            .HasData(
                new Order[]
                {
                    new Order
                    {
                        Id = 1,
                        CashierId = 1,
                        PaidOnDate = DateTime.Parse("2024-02-04 10:30:00"),
                    },
                    new Order
                    {
                        Id = 2,
                        CashierId = 2,
                        PaidOnDate = DateTime.Parse("2024-02-04 11:45:00"),
                    },
                    new Order
                    {
                        Id = 3,
                        CashierId = 1,
                        PaidOnDate = DateTime.Parse("2024-02-04 14:15:00"),
                    },
                }
            );

        modelBuilder
            .Entity<OrderProduct>()
            .HasData(
                new OrderProduct[]
                {
                    // Order 1: Cola and Chips
                    new OrderProduct
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 2,
                    },
                    new OrderProduct
                    {
                        OrderId = 1,
                        ProductId = 2,
                        Quantity = 1,
                    },
                    // Order 2: Milk and Bananas
                    new OrderProduct
                    {
                        OrderId = 2,
                        ProductId = 3,
                        Quantity = 1,
                    },
                    new OrderProduct
                    {
                        OrderId = 2,
                        ProductId = 4,
                        Quantity = 3,
                    },
                    // Order 3: Energy Drink and Chocolate Bar
                    new OrderProduct
                    {
                        OrderId = 3,
                        ProductId = 5,
                        Quantity = 2,
                    },
                    new OrderProduct
                    {
                        OrderId = 3,
                        ProductId = 6,
                        Quantity = 4,
                    },
                }
            );
    }
}

using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using CornerStore.Models;
using CornerStore.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(
    builder.Configuration["CornerStoreDbConnectionString"] ?? "testing"
);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//endpoints go here

app.MapPost(
    "/cashiers",
    (AddCashierDto addCashier, CornerStoreDbContext db) =>
    {
        Cashier cashier = new Cashier
        {
            FirstName = addCashier.FirstName,
            LastName = addCashier.LastName,
        };

        db.Cashiers.Add(cashier);
        db.SaveChanges();
        return Results.Created($"/cashiers/{cashier.Id}", cashier);
    }
);
app.MapGet(
    "/cashiers/{id}",
    (CornerStoreDbContext db, int id) =>
    {
        Cashier? cashier = db
            .Cashiers.Include(c => c.Orders)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefault(c => c.Id == id);

        if (cashier == null)
        {
            return Results.NotFound();
        }
        ;

        return Results.Ok(
            new CashierDto
            {
                Id = cashier.Id,
                FirstName = cashier.FirstName,
                LastName = cashier.LastName,
                Orders = cashier
                    .Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        PaidOnDate = o.PaidOnDate,
                        OrderProducts = o
                            .OrderProducts.Select(op => new OrderProductDto
                            {
                                ProductId = op.ProductId,
                                Quantity = op.Quantity,
                                Product = new ProductDto
                                {
                                    Id = op.Product.Id,
                                    ProductName = op.Product.ProductName,
                                    Price = op.Product.Price,
                                },
                            })
                            .ToList(),
                    })
                    .ToList(),
            }
        );
    }
);
app.MapGet(
    "/products",
    (string? search, CornerStoreDbContext db) =>
    {
        IQueryable<Product> query = db.Products.Include(p => p.Category);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p =>
                p.ProductName.ToLower().Contains(search.ToLower())
                || p.Category.CategoryName.ToLower().Contains(search.ToLower())
            );
        }
        return query.ToList();
    }
);
app.MapPost(
    "/products",
    (CornerStoreDbContext db, Product Product) =>
    {
        Product product = new Product
        {
            ProductName = Product.ProductName,
            Price = Product.Price,
            Brand = Product.Brand,
        };
        db.Products.Add(product);
        db.SaveChanges();
        return Results.Created($"/products/{product.Id}", product);
    }
);
app.MapPut(
    "/products/{id}",
    (int id, CornerStoreDbContext db, Product Product) =>
    {
        Product? productToUpdate = db.Products.SingleOrDefault(product => product.Id == id);
        if (productToUpdate == null)
        {
            return Results.NotFound();
        }
        productToUpdate.ProductName = Product.ProductName;
        productToUpdate.Price = Product.Price;
        productToUpdate.Brand = Product.Brand;
        productToUpdate.CategoryId = Product.CategoryId;

        db.SaveChanges();
        return Results.NoContent();
    }
);
app.MapGet(
    "/orders/{id}",
    (int id, CornerStoreDbContext db) =>
    {
        Order order = db
            .Orders.Include(o => o.Cashier)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            return Results.NotFound();
        }
        ;

        return Results.Ok(
            new OrderDto
            {
                Id = order.Id,
                CashierId = order.CashierId,
                PaidOnDate = order.PaidOnDate,
                Cashier = new CashierDto
                {
                    Id = order.Cashier.Id,
                    FirstName = order.Cashier.FirstName,
                    LastName = order.Cashier.LastName,
                },
                OrderProducts = order
                    .OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        OrderId = op.OrderId,
                        Quantity = op.Quantity,
                        Product = new ProductDto
                        {
                            Id = op.Product.Id,
                            ProductName = op.Product.ProductName,
                            Price = op.Product.Price,
                            Brand = op.Product.Brand,
                            Category = new CategoryDto
                            {
                                Id = op.Product.Category.Id,
                                CategoryName = op.Product.Category.CategoryName,
                            },
                        },
                    })
                    .ToList(),
            }
        );
    }
);
app.MapDelete(
    "/orders/{id}",
    (int id, CornerStoreDbContext db) =>
    {
        Order? Order = db.Orders.SingleOrDefault(Order => Order.Id == id);
        if (Order == null)
        {
            return Results.NoContent();
        }
        db.Orders.Remove(Order);
        db.SaveChanges();
        return Results.NoContent();
    }
);

app.MapGet(
    "/orders",
    (CornerStoreDbContext db, string orderDate) =>
    {
        IQueryable<Order> query = db.Orders.Include(o => o.Cashier);

        if (!string.IsNullOrEmpty(orderDate))
        {
            if (DateTime.TryParse(orderDate, out DateTime parsedDate))
            {
                // Filter orders for the specified date
                query = query.Where(o => o.PaidOnDate.Date == parsedDate.Date);
            }
        }

        return query.Select(o => new OrderDto
        {
            Id = o.Id,
            PaidOnDate = o.PaidOnDate,
            CashierId = o.CashierId,
            Cashier = new CashierDto
            {
                Id = o.Cashier.Id,
                FirstName = o.Cashier.FirstName,
                LastName = o.Cashier.LastName,
            },
        });
    }
);

app.MapPost(
    "/orders",
    (CornerStoreDbContext db, Order OrderRequest) =>
    {
        Order newOrder = new Order
        {
            CashierId = OrderRequest.CashierId,
            PaidOnDate = DateTime.Now,
            OrderProducts = new List<OrderProduct>(),
        };
        foreach (var requestProduct in OrderRequest.OrderProducts)
        {
            var productFromDb = db.Products.Find(requestProduct.ProductId);
            var orderProduct = new OrderProduct
            {
                ProductId = requestProduct.ProductId,
                OrderId = newOrder.Id,
                Quantity = requestProduct.Quantity,
            };
            newOrder.OrderProducts.Add(orderProduct);
        }
        db.Orders.Add(newOrder);
        db.SaveChanges();

        var savedOrder = db
            .Orders.Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefault(o => o.Id == newOrder.Id);

        return Results.Created($"/orders/{savedOrder.Id}", savedOrder);
    }
);

app.MapGet(
    "/",
    () =>
    {
        return Results.Redirect("/swagger");
    }
);
app.Run();

//don't move or change this!
public partial class Program { }

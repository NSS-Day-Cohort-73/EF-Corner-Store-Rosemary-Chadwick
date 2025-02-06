using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; }

    public int CashierId { get; set; }

    [Required]
    public DateTime PaidOnDate { get; set; }

    public Cashier Cashier { get; set; }

    public List<OrderProduct> OrderProducts { get; set; } = new();

    public decimal Total
    {
        get
        {
            decimal orderTotal = 0M;

            if (OrderProducts != null)
            {
                foreach (var orderProduct in OrderProducts)
                {
                    if (orderProduct.Product != null)
                    {
                        decimal productTotal = orderProduct.Product.Price * orderProduct.Quantity;
                        orderTotal += productTotal;
                    }
                }
            }
            return orderTotal;
        }
    }
};

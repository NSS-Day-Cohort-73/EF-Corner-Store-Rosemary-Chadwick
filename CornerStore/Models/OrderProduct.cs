using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace CornerStore.Models;

public class OrderProduct
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Product Product { get; set; }

    public Order Order { get; set; }
}

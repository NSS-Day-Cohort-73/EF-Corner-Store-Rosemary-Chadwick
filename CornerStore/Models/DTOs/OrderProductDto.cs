namespace CornerStore.Models.DTOs;

public class OrderProductDto
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public ProductDto Product { get; set; }

    public OrderDto Order { get; set; }
}

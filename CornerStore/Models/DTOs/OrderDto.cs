namespace CornerStore.Models.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public int CashierId { get; set; }

    public DateTime PaidOnDate { get; set; }

    public CashierDto Cashier { get; set; }

    public List<OrderProductDto> OrderProducts { get; set; }
}

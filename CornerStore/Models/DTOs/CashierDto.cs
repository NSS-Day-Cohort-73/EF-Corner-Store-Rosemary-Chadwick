namespace CornerStore.Models.DTOs;

public class CashierDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public List<OrderDto> Orders { get; set; }
}

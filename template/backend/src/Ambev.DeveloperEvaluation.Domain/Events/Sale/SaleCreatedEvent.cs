public class SaleCreatedEvent
{
    public Guid SaleId { get; set; }
    public Guid UserId { get; set; }
    public string CustomerId { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public List<SaleItemData> Items { get; set; } = new();
}

public class SaleItemData
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
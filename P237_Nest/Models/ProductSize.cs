namespace P237_Nest.Models;

public class ProductSize
{
    public int Id { get; set; }
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
    public Size Size { get; set; } = null!;
    public int SizeId { get; set; }
    public int Count { get; set; }
}

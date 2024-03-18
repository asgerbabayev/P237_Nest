using System.ComponentModel.DataAnnotations.Schema;

namespace P237_Nest.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    //[Range(0, 6)]
    public double? Rating { get; set; }
    public decimal SellPrice { get; set; } = default!;
    public decimal? DiscountPrice { get; set; }
    [NotMapped]
    public List<IFormFile>? Files { get; set; }
    [NotMapped]
    public IFormFile MainFile { get; set; }
    [NotMapped]
    public IFormFile HoverFile { get; set; }
    public List<ProductImage>? ProductImages { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<ProductSize>? ProductSizes { get; set; }
    public Product()
    {
        ProductSizes = new HashSet<ProductSize>();
    }
}

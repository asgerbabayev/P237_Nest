using P237_Nest.Models;

namespace P237_Nest.ViewModels;

public class ProductSearchVm
{
    public string? Name { get; set; }
    public int? CategoryId { get; set; }
    public List<Category> Categories { get; set; }
}

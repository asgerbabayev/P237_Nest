using P237_Nest.Models;

namespace P237_Nest.ViewModels;

public class PaginateVm
{
    public int TotalPageCount { get; set; }
    public int CurrentPage { get; set; }
    public List<Product> Products { get; set; }
}

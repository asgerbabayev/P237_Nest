using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;

namespace P237_Nest.ViewComponents;

public class ProductViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public ProductViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
    {
        var products = _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductImages);
        return categoryId == null ? View(await products.OrderByDescending(x => x.Id).Take(20).ToListAsync())
                                  : View(await products.Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.Id).Take(20).ToListAsync());
    }


}

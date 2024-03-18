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

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductImages)
            .OrderByDescending(x => x.Id).Take(20).ToListAsync();
        return View(products);
    }


}

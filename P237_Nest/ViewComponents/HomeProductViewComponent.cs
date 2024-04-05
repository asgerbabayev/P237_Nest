using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;

namespace P237_Nest.ViewComponents;

public class HomeProductViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public HomeProductViewComponent(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
    {
        if (categoryId == null)
        {
            return View(await _context.Products
                .Take(20)
                .Include(x => x.Category)
                .Include(x => x.ProductImages).ToListAsync());
        }
        var products = await _context.Products.Where(x => x.CategoryId == categoryId)
            .Take(20)
            .Include(x => x.Category)
            .Include(x => x.ProductImages).ToListAsync();

        return View(products);
    }
}

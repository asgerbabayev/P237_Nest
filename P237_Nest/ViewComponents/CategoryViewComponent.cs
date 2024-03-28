using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;

namespace P237_Nest.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public CategoryViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Categories.Include(x => x.Products).ToListAsync();
        return View(categories);
    }
}

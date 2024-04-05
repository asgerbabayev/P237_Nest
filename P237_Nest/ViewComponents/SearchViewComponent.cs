using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;
using P237_Nest.ViewModels;

namespace P237_Nest.ViewComponents;

public class SearchViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public SearchViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ProductSearchVm vm = new ProductSearchVm()
        {
            Categories = await _context.Categories.ToListAsync()
        };
        return View(vm);
    }
}



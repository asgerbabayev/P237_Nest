using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;

namespace P237_Nest.ViewComponents
{
    public class HomeCategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeCategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}

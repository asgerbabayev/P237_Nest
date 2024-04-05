using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;
using P237_Nest.ViewModels;

namespace P237_Nest.ViewComponents;

public class ProductViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public ProductViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? categoryId, string productName, int page = 1, int pageSize = 1)
    {
        var products = _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductImages);

        var count = GetPageCount(pageSize);
        PaginateVm paginateVm = new PaginateVm()
        {
            CurrentPage = page,
            TotalPageCount = count,
        };
        if (categoryId == null)
        {
            var orderedWithCatProducts = await products.Skip((page - 1) * pageSize)
                                                       .Take(pageSize)
                                                       .OrderByDescending(x => x.Id)
                                                       .ToListAsync();

            paginateVm.Products = orderedWithCatProducts;

            return View(paginateVm);
        }
        if (productName != null)
        {
            paginateVm.Products = await products.Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .Where(x => x.CategoryId == categoryId && x.Name.StartsWith(productName))
                                                .OrderByDescending(x => x.Id)
                                                .ToListAsync();
            return View(paginateVm);
        }
        var orderedProducts = await products.Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .Where(x => x.CategoryId == categoryId)
                                                .OrderByDescending(x => x.Id)
                                                .ToListAsync();
        paginateVm.Products = orderedProducts;


        return View(paginateVm);
    }

    public int GetPageCount(int pageSize)
    {
        var productCount = _context.Products.Count();
        return (int)Math.Ceiling((decimal)productCount / pageSize);
    }
}

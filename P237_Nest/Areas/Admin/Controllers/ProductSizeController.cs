using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Areas.Admin.ViewModels;
using P237_Nest.Data;
using P237_Nest.Models;

namespace P237_Nest.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class ProductSizeController : Controller
{
    private readonly AppDbContext _context;

    public ProductSizeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var prSizes = await _context.ProductSizes.Include(x => x.Product).Include(x => x.Size).ToListAsync();
        return View(prSizes);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Sizes = await _context.Sizes.ToListAsync();
        ViewBag.Products = await _context.Products.ToListAsync();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductSizeVm productSizeVm)
    {
        if (!ModelState.IsValid) return View(productSizeVm);

        var exist = await _context.ProductSizes.Include(x => x.Size).AnyAsync(x => x.SizeId == productSizeVm.SizeId);
        if (exist)
        {
            ModelState.AddModelError("", "Size already exist");
            return View(productSizeVm);
        }
        _context.ProductSizes.Add((ProductSize)productSizeVm);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;
using P237_Nest.Models;

namespace P237_Nest.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SizeController : Controller
{
    private readonly AppDbContext _context;

    public SizeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sizes = await _context.Sizes.ToListAsync();
        return View(sizes);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Size size)
    {
        size.Name = size.Name.ToUpper().Trim();
        if (_context.Sizes.Any(x => x.Name.ToUpper().Trim() == size.Name))
        {
            ModelState.AddModelError("", "Size already exist");
            return View();
        }
        await _context.Sizes.AddAsync(size);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}

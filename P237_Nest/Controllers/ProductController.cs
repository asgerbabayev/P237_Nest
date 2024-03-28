using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P237_Nest.Data;
using P237_Nest.ViewModels;

namespace P237_Nest.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null) return NotFound();
        var product = await _context.Products
           .Include(x => x.Category)
           .Include(x => x.ProductImages)
           .Include(x => x.ProductSizes)
           .ThenInclude(x => x.Size)
           .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null) return NotFound();
        var categories = await _context.Categories.Include(x => x.Products).ToListAsync();
        ProductVm productVm = new ProductVm()
        {

            Product = product,
            Categories = categories
        };
        return View(productVm);
    }

    public async Task<IActionResult> AddToCart(int id)
    {
        var existProduct = await _context.Products.AnyAsync(x => x.Id == id);
        if (!existProduct) return NotFound();

        List<BasketVm>? basketVm = GetBasket();
        BasketVm cartVm = basketVm.Find(x => x.Id == id);
        if (cartVm != null)
        {
            cartVm.Count++;
        }
        else
        {
            basketVm.Add(new BasketVm
            {
                Count = 1,
                Id = id
            });
        }
        Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketVm));
        return RedirectToAction("Index");
    }
    private List<BasketVm> GetBasket()
    {
        List<BasketVm> basketVms;
        if (Request.Cookies["basket"] != null)
        {
            basketVms = JsonConvert.DeserializeObject<List<BasketVm>>(Request.Cookies["basket"]);
        }
        else basketVms = new List<BasketVm>();
        return basketVms;
    }
}


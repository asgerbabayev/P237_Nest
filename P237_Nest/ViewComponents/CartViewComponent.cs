using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P237_Nest.Data;
using P237_Nest.ViewModels;

namespace P237_Nest.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public CartViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<BasketVm>? basketVm = GetBasket();
        List<BasketItemsVm> basketItemsVm = new List<BasketItemsVm>();
        foreach (var basketData in basketVm)
        {
            var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == basketData.Id);
            basketItemsVm.Add(new BasketItemsVm()
            {
                Count = basketData.Count,
                Id = basketData.Id,
                Image = product.ProductImages.FirstOrDefault(x => x.IsMain).Url,
                Price = product.SellPrice,
                Name = product.Name,
            });
        }

        return View(basketItemsVm);
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

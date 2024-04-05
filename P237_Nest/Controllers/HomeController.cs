using Microsoft.AspNetCore.Mvc;

namespace P237_Nest.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductCategoryFilter(int? id)
    {
        return ViewComponent("HomeProduct", new { categoryId = id });
    }
}

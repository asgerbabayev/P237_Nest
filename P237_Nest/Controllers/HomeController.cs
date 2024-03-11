using Microsoft.AspNetCore.Mvc;

namespace P237_Nest.Controllers;

public class HomeController : Controller
{


    public IActionResult Index()
    {
        return View();
    }
}

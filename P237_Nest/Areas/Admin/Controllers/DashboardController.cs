using Microsoft.AspNetCore.Mvc;

namespace P237_Nest.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

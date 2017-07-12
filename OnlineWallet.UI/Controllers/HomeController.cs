using Microsoft.AspNetCore.Mvc;
using OnlineWallet.UI.Framework.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

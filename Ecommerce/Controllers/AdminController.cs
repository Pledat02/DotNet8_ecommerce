using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : BaseController
    {   
        public  IActionResult Index()
        {

            return View();
        }
    }
}

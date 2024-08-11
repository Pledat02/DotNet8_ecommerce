using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ecommerce.Helper;
namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cart = HttpContext.Session.Get<List<CartItemViewModel>>(SettingKey.CART_KEY) ?? new List<CartItemViewModel>();
            ViewBag.SizeCart = cart.Count;

            base.OnActionExecuting(filterContext);
        }
    }
}

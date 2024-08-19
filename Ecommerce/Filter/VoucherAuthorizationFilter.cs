using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Filter
{
    public class VoucherAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // Nếu chưa xác thực, chuyển hướng đến trang đăng nhập
                context.Result = new JsonResult(new { redirectTo = "/Home/Login" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (!user.IsInRole("VoucherManager"))
            {
                // Nếu không có quyền, trả về mã lỗi 403
                context.Result = new JsonResult(new { redirectTo = "/Home/Error403" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            base.OnActionExecuting(context);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Services
{
    public class AuthenticationAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            string? token = context.HttpContext.Session.GetString("_Token");
            int? role = context.HttpContext.Session.GetInt32("_Role");

            if (String.IsNullOrEmpty(token) && (role <= 0 || role == null))
                context.Result = new RedirectToActionResult("Login", "Auth", "");
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JProyecto.Services
{
    public class Sesiones : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("JWT") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

    }


    public class Administradores : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("IdRol") != "2")
            {
                context.Result = new RedirectToActionResult("Principal", "Home", null);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

    }
}

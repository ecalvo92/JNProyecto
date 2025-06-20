using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace JProyecto.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult CapturarError()
        {
            HttpContext.Features.Get<IExceptionHandlerFeature>();

            return View("Error");
        }
    }
}

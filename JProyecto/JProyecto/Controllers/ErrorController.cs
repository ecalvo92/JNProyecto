using Microsoft.AspNetCore.Mvc;

namespace JProyecto.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult CapturarError()
        {
            return View("Error");
        }
    }
}

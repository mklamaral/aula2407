using Microsoft.AspNetCore.Mvc;

namespace AulaDeASPNet.Controllers
{
    public class NossoAppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

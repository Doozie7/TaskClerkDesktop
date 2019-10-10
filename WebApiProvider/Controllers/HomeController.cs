using System.Web.Mvc;

namespace WebApiProvider.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

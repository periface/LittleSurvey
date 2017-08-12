using System.Web.Mvc;

namespace LittleSurvey.Web.Controllers
{
    public class HomeController : LittleSurveyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
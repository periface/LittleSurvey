using System.Web.Mvc;

namespace LittleSurvey.Web.Controllers
{
    public class AboutController : LittleSurveyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
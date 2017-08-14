using System.Web.Mvc;

namespace Survey.Web.Controllers
{
    public class AnswersAdminController : SurveyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
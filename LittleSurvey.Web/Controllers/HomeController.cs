using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace LittleSurvey.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LittleSurveyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
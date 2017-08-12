using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Web.Controllers
{
    public class SurveyAdminController : SurveyControllerBase
    {
        // GET: SurveyAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}
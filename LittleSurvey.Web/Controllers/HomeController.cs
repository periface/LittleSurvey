using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Survey.Application.SurveyService;

namespace LittleSurvey.Web.Controllers
{
    public class HomeController : LittleSurveyControllerBase
    {
        private readonly ISurveyAppService _surveyAppService;

        public HomeController(ISurveyAppService surveyAppService)
        {
            _surveyAppService = surveyAppService;
        }

        public  async Task<ActionResult> Index()
        {
            var surveys = await _surveyAppService.GetSurveys();
            return View(surveys);
        }

        public async Task<ActionResult> Survey(string surveyurl)
        {
            var survey = await _surveyAppService.GetSurveyFirstQuestion(surveyurl);
            return View(survey);
        }

        public ActionResult GetNextQuestion(int questionId, int surveyId)
        {
            var question = _surveyAppService.GetQuestion(surveyId,questionId);
            return View(question);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Survey.Application.SurveyService;
using Survey.Application.SurveyService.Dto;

namespace Survey.Web.Controllers
{
    public class SurveyAdminController : SurveyControllerBase
    {
        private readonly ISurveyAppService _surveyAppService;

        public SurveyAdminController(ISurveyAppService surveyAppService)
        {
            _surveyAppService = surveyAppService;
        }

        // GET: SurveyAdmin
        public async Task<ActionResult> Index()
        {
            var surveys = await _surveyAppService.GetSurveys();
            return View(surveys);
        }

        public async Task<ActionResult> AddQuestions(int id)
        {
            var availableQuestions = _surveyAppService.GetAvailableQuestions(id);
            List<QuestionDto> questions = _surveyAppService.GetAssignedQuestions(id);
            var survey = await _surveyAppService.GetSurveyById(id);

            return View(new SetQuestionsDto(availableQuestions, questions, survey));
        }
    }
}
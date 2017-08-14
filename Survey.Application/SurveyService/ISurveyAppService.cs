using System.Collections.Generic;
using Abp.Application.Services;
using System.Threading.Tasks;
using System.Web.Http;
using Survey.Application.SurveyService.Dto;

namespace Survey.Application.SurveyService
{
    public interface ISurveyAppService : IApplicationService
    {
        [HttpPost]
        Task<int> CreateEditSurvey(SurveyInputDto input);
        [HttpDelete]
        Task DeleteSurvey(int id);
        [HttpPost]
        Task<int> CreateQuestion(QuestionInputDto input);
        [HttpPut]
        Task AssignQuestionToSurvey(int surveyId, int questionId);
        [HttpPut]
        Task AssignQuestionsToSurvey(List<AssignQuestionInputDto> input, int surveyId);
        [HttpDelete]
        Task RemoveQuestionFromSurvey(int surveyId, int questionId);
        [HttpGet]
        Task<SurveyForUserDto> GetSurvey(string url);
        [HttpGet]
        Task<SurveyWithOnlyFirstQuestionForUserDto> GetSurveyFirstQuestion(string url);
        [HttpGet]
        Task<List<SurveyDto>> GetSurveys();
        [HttpPost]
        Task<int> AddPredefinedAnswer(string txt);
        [HttpPut]
        Task SetOfferedAnswer(int surveyId, int questionId, int idAnswer);
        [HttpPost]
        Task AnswerQuestion(AnswerInputDto answer);
        [HttpGet]
        QuestionDto GetQuestion(int surveyId, int questionId);
        [HttpPost]
        Task BulkAnswer(List<AnswerInputDto> answers);
        [HttpGet]
        List<QuestionDto> GetAvailableQuestions(int id);
        [HttpGet]
        Task<SurveyDto> GetSurveyById(int id);
        [HttpGet]
        List<QuestionDto> GetAssignedQuestions(int id);
    }
}

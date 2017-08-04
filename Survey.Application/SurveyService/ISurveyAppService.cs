using Abp.Application.Services;
using System.Threading.Tasks;
using Survey.Application.SurveyService.Dto;

namespace Survey.Application.SurveyService
{
    public interface ISurveyAppService : IApplicationService
    {
        Task<int> CreateEditSurvey(SurveyInputDto input);
        Task<int> CreateQuestion(QuestionInputDto input);
        Task AssignQuestionToSurvey(int surveyId, int questionId);
        Task RemoveQuestionFromSurvey(int surveyId, int questionId);
        Task<SurveyForUserDto> GetSurvey(string url);
        Task<List<SurveyDto>> GetSurveys();
        Task<int> AddPredefinedAnswer(string txt);
        Task SetOfferedAnswer(int surveyId, int questionId, int idAnswer);
        Task AnswerQuestion(AnswerInputDto answer);

        int GetAllAnswers();

        Task BulkAnswer(List<AnswerInputDto> answers);
    }
}

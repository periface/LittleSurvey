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
        Task<int> AddPredefinedAnswer(string bueno);
        Task SetOfferedAnswer(int surveyId, int questionId, int idMalo);
    }
}

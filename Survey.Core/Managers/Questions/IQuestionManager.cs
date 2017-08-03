using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Survey.Core.Entities;

namespace Survey.Core.Managers.Questions
{
    public interface IQuestionManager : IDomainService
    {
        Task<int> CreateQuestionAsync(string text);
        int CreateQuestion(string text);


        Task EditQuestionAsync(int questionId, string text);
        void EditQuestion(int questionId, string text);


        Task<int> RemoveQuestionAsync(int questionId, string text);
        int RemoveQuestion(int questionId, string text);


        Task SetSurveyAsync(int questionId,int surveyId);
        void SetSurvey(int questionId, int surveyId);
        Task<List<Question>> GetQuestionsFromSurvey(int surveyId);
        Task Answer(int surveyId, int questionId, int[] offeredAnswersId, string otherText, long? abpSessionUserId);
        Task<int> CreatePredefinedAnswer(string txt);
        Task SetPredefinedAnswer(int surveyId, int questionId, int idPredefinedAnswer);
    }
}

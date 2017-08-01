using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

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
    }
}

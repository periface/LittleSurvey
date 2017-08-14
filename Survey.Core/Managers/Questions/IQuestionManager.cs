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
        Task<int> CreateQuestionAsync(string text, bool inputAllowMultipleAnswers);
        int CreateQuestion(string text);


        Task EditQuestionAsync(int questionId, string text);
        void EditQuestion(int questionId, string text);


        Task<int> RemoveQuestionAsync(int questionId, string text);
        int RemoveQuestion(int questionId, string text);


        Task SetSurveyAsync(int questionId,int surveyId);
        void SetSurvey(int questionId, int surveyId);
        Task<List<Question>> GetQuestionsFromSurvey(int surveyId);
        Task Answer(int surveyId, int questionId, int[] offeredAnswersId, string otherText, long? abpSessionUserId);
        /// <summary>
        /// Creates a new predefined answer
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        Task<int> CreatePredefinedAnswer(string txt);
        /// <summary>
        /// Sets the predefined answer for the question in the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="questionId"></param>
        /// <param name="idPredefinedAnswer"></param>
        /// <returns></returns>
        Task SetPredefinedAnswer(int surveyId, int questionId, int idPredefinedAnswer);
        /// <summary>
        /// Get all assigned questions for the survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Question> GetQuestionsForSurveyAsync(int id);
        /// <summary>
        /// Get all assigned questions for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        List<Question> GetQuestionsForSurveyInvertedAsync(int surveyId);
    }
}

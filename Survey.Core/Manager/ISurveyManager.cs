using System.Threading.Tasks;
using Abp.Domain.Services;
using Survey.Core.Entities;

namespace Survey.Core.Manager
{
    public interface ISurveyManager : IDomainService
    {
        /// <summary>
        /// Creates a new survey
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        Task CreateSurveyAsync(Entities.Survey survey);
        /// <summary>
        /// Creates a new survey
        /// </summary>
        void CreateSurvey(Entities.Survey survey);
        /// <summary>
        /// Edits the current survey
        /// </summary>
        Task EditSurveyAsync(Entities.Survey survey);
        /// <summary>
        /// Edits the current survey
        /// </summary>
        void EditSurvey(Entities.Survey survey);
        /// <summary>
        /// Deletes the current survey
        /// </summary>
        Task DeleteSurveyAsync(Entities.Survey survey);
        /// <summary>
        /// Deletes the current survey
        /// </summary>
        void DeleteSurvey(Entities.Survey survey);

        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task AddQuestionAsync(int questionId);
        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task AddQuestionAsync(Question question);

        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        void AddQuestion(int questionId);
        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        void AddQuestion(Question question);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveQuestionAsync(int id);
        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task RemoveQuestionAsync(Question question);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveQuestion(int id);
        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task RemoveQuestion(Question question);
    }
}

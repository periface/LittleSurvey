using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Survey.Core.Entities;

namespace Survey.Core.Managers.Surveys
{
    public interface ISurveyManager : IDomainService
    {
        /// <summary>
        /// Creates a new survey
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        Task<int> CreateSurveyAsync(Entities.Survey survey);
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
        /// <param name="surveyId"></param>
        /// <returns></returns>
        Task AddQuestionAsync(int questionId, int surveyId);

        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="question"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        Task AddQuestionAsync(Question question, int surveyId);

        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        void AddQuestion(int questionId, int surveyId);

        /// <summary>
        /// Sets the requested question to the survey
        /// </summary>
        /// <param name="question"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        void AddQuestion(Question question, int surveyId);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="id"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        Task RemoveQuestionAsync(int id, int surveyId);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="question"></param>
        /// <param name="survey"></param>
        /// <returns></returns>
        Task RemoveQuestionAsync(Question question, Entities.Survey survey);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="id"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        void RemoveQuestion(int id,int surveyId);

        /// <summary>
        /// Removes the question from the survey
        /// </summary>
        /// <param name="question"></param>
        /// <param name="survey"></param>
        /// <returns></returns>
        void RemoveQuestion(Question question,Entities.Survey survey);
        /// <summary>
        /// Gets the survey from its unique url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Entities.Survey GetSurveyFromUrl(string url);
        /// <summary>
        /// Gets all the questions with their offered answers
        /// <para>If the user had answered the question then it will come with it</para>
        /// </summary>
        /// <param name="abpSessionUserId"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        IDictionary<QuestionWithOffered, Answer> GetQuestionsWithAnswers(long? abpSessionUserId, int surveyId);
        /// <summary>
        /// Get all unassigned questions for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        List<Question> GetQuestions(int surveyId);
        
        /// <summary>
        /// Gets all the questions with their offered answers
        /// <para>If the user had answered the question then it will come with it</para>
        /// </summary>
        /// <param name="abpSessionUserId"></param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        Task<IDictionary<QuestionWithOffered, Answer>> GetQuestionsWithAnswersAsync(long? abpSessionUserId, int surveyId);
        /// <summary>
        /// Get all unassigned questions for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        Task<List<Question>> GetQuestionsAsync(int surveyId);
        /// <summary>
        /// Gets the survey from its unique url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<Entities.Survey> GetSurveyFromUrlAsync(string url);

        Task AddQuestionsAsync(int surveyId, List<int> questions);
    }
}

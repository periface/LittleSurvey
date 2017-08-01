using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Survey.Core.Entities;
using Survey.Core.Manager;

namespace Survey.Core.Managers.Surveys
{
    public class SurveyManager : ISurveyManager
    {
        private readonly IRepository<Entities.Survey> _surveyRepository;
        private readonly IRepository<Question> _questionRepository;
        public SurveyManager(IRepository<Entities.Survey> surveyRepository, IRepository<Question> questionRepository)
        {
            _surveyRepository = surveyRepository;
            this._questionRepository = questionRepository;
        }

        public Task CreateSurveyAsync(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public void CreateSurvey(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public Task EditSurveyAsync(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public void EditSurvey(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSurveyAsync(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSurvey(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public Task AddQuestionAsync(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddQuestionAsync(Question question)
        {
            throw new System.NotImplementedException();
        }

        public void AddQuestion(int questionId)
        {
            throw new System.NotImplementedException();
        }

        public void AddQuestion(Question question)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveQuestionAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveQuestionAsync(Question question)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveQuestion(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveQuestion(Question question)
        {
            throw new System.NotImplementedException();
        }
    }
}

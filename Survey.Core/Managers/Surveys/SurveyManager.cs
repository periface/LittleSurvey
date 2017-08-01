using System.Linq;
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
        private readonly IRepository<SurveyQuestion> _surveyQuestionRepository;
        public SurveyManager(IRepository<Entities.Survey> surveyRepository, IRepository<Question> questionRepository, IRepository<SurveyQuestion> surveyQuestionRepository)
        {
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            this._surveyQuestionRepository = surveyQuestionRepository;
        }

        public Task CreateSurveyAsync(Entities.Survey survey)
        {
            return _surveyRepository.InsertOrUpdateAndGetIdAsync(survey);
        }

        public void CreateSurvey(Entities.Survey survey)
        {
            _surveyRepository.InsertOrUpdateAndGetId(survey);
        }

        public Task EditSurveyAsync(Entities.Survey survey)
        {
            return _surveyRepository.InsertOrUpdateAndGetIdAsync(survey);
        }

        public void EditSurvey(Entities.Survey survey)
        {
            _surveyRepository.InsertOrUpdateAndGetId(survey);
        }

        public Task DeleteSurveyAsync(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSurvey(Entities.Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public Task AddQuestionAsync(int questionId,int surveyId)
        {
            //Question is already asigned
            if (_surveyQuestionRepository.GetAll().Any(a => a.QuestionId == questionId)) return Task.CompletedTask;

            var question = _questionRepository.FirstOrDefault(a => a.Id == questionId);

            if(question == null) return Task.CompletedTask;

            return _surveyQuestionRepository.InsertOrUpdateAndGetIdAsync(new SurveyQuestion(surveyId,questionId));
            
        }

        public Task AddQuestionAsync(Question question,int surveyId)
        {
            //Question is already asigned
            if (_surveyQuestionRepository.GetAll().Any(a => a.QuestionId == question.Id)) return Task.CompletedTask;

            if (question == null) return Task.CompletedTask;

            return _surveyQuestionRepository.InsertOrUpdateAndGetIdAsync(new SurveyQuestion(surveyId, question.Id));
        }

        public void AddQuestion(int questionId,int surveyId)
        {
            //Question is already asigned
            if (_surveyQuestionRepository.GetAll().Any(a => a.QuestionId == questionId)) return;

            var question = _questionRepository.FirstOrDefault(a => a.Id == questionId);

            if (question == null) return;

            _surveyQuestionRepository.InsertOrUpdateAndGetId(new SurveyQuestion(surveyId, questionId));
        }

        public void AddQuestion(Question question,int surveyId)
        {
            //Question is already asigned
            if (_surveyQuestionRepository.GetAll().Any(a => a.QuestionId == question.Id)) return;

            if (question == null) return;

            _surveyQuestionRepository.InsertOrUpdateAndGetId(new SurveyQuestion(surveyId, question.Id));
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

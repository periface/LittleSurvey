using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Components.DictionaryAdapter;
using Survey.Core.Entities;

namespace Survey.Core.Managers.Surveys
{
    public class SurveyManager : ISurveyManager
    {
        private readonly IRepository<Entities.Survey> _surveyRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<SurveyQuestion> _surveyQuestionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<SurveyQuestionAnswer> _surveyQuestionAnswerRepository;
        public SurveyManager(IRepository<Entities.Survey> surveyRepository, IRepository<Question> questionRepository, IRepository<SurveyQuestion> surveyQuestionRepository, IRepository<Answer> answerRepository, IRepository<SurveyQuestionAnswer> surveyQuestionAnswerRepository)
        {
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _answerRepository = answerRepository;
            _surveyQuestionAnswerRepository = surveyQuestionAnswerRepository;
        }

        public Task<int> CreateSurveyAsync(Entities.Survey survey)
        {

            if (_surveyRepository.GetAllList(a => a.SurveyUrl.ToUpper().Contains(survey.SurveyUrl.ToUpper())).Any())
            {
                throw new UserFriendlyException($"Ya hay una encuesta con esta url: {survey.SurveyUrl}");
            }

            return _surveyRepository.InsertOrUpdateAndGetIdAsync(survey);
        }

        public void CreateSurvey(Entities.Survey survey)
        {
            if (_surveyRepository.GetAllList(a => a.SurveyUrl.ToUpper().Contains(survey.SurveyUrl.ToUpper())).Any())
            {
                throw new UserFriendlyException($"Ya hay una encuesta con esta url: {survey.SurveyUrl}");
            }
            _surveyRepository.InsertOrUpdateAndGetId(survey);
        }

        public Task EditSurveyAsync(Entities.Survey survey)
        {
            if (_surveyRepository.GetAllList(a => a.SurveyUrl.ToUpper().Contains(survey.SurveyUrl.ToUpper()) && a.Id != survey.Id).Any())
            {
                throw new UserFriendlyException($"Ya hay una encuesta con esta url: {survey.SurveyUrl}");
            }
            return _surveyRepository.InsertOrUpdateAndGetIdAsync(survey);
        }

        public void EditSurvey(Entities.Survey survey)
        {
            if (_surveyRepository.GetAllList(a => a.SurveyUrl.ToUpper().Contains(survey.SurveyUrl.ToUpper()) && a.Id != survey.Id).Any())
            {
                throw new UserFriendlyException($"Ya hay una encuesta con esta url: {survey.SurveyUrl}");
            }
            _surveyRepository.InsertOrUpdateAndGetId(survey);
        }

        public Task DeleteSurveyAsync(Entities.Survey survey)
        {
            return _surveyRepository.DeleteAsync(survey);
        }

        public void DeleteSurvey(Entities.Survey survey)
        {
            _surveyRepository.Delete(survey);
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

        public Task RemoveQuestionAsync(int id,int surveyId)
        {
            var questionAssignment = _surveyQuestionRepository.FirstOrDefault(a => a.QuestionId == id && a.SurveyId == surveyId);
            if(questionAssignment == null) return Task.CompletedTask;
            return _surveyQuestionRepository.DeleteAsync(questionAssignment);
        }

        public Task RemoveQuestionAsync(Question question,Entities.Survey survey)
        {
            var questionAssignment = _surveyQuestionRepository.FirstOrDefault(a => a.QuestionId == question.Id && a.SurveyId == survey.Id);
            if (questionAssignment == null) return Task.CompletedTask;
            return _surveyQuestionRepository.DeleteAsync(questionAssignment);
        }

        public void RemoveQuestion(int id, int surveyId)
        {
            var questionAssignment = _surveyQuestionRepository.FirstOrDefault(a => a.QuestionId == id && a.SurveyId == surveyId);
            if (questionAssignment == null) return;
            _surveyQuestionRepository.Delete(questionAssignment);
        }

        public void RemoveQuestion(Question question, Entities.Survey survey)
        {
            var questionAssignment = _surveyQuestionRepository.FirstOrDefault(a => a.QuestionId == question.Id && a.SurveyId == survey.Id);
            if (questionAssignment == null) return;
            _surveyQuestionRepository.Delete(questionAssignment);
        }

        public Entities.Survey GetSurveyFromUrl(string url)
        {
            var survey = _surveyRepository.FirstOrDefault(a => a.SurveyUrl == url);

            return survey;
        }

        public List<Question> GetQuestions(int surveyId)
        {
            var questionAssignment = _surveyQuestionRepository.GetAllList(a => a.SurveyId == surveyId);
            List<Question> questions = new EditableList<Question>();
            foreach (var surveyQuestion in questionAssignment)
            {
                var question = _questionRepository.Get(surveyQuestion.QuestionId);

                questions.Add(question);
            }
            return questions;
        }
        public IDictionary<Question, Answer> GetQuestionsWithAnswers(long? abpSessionUserId, int surveyId)
        {
            var result = new Dictionary<Question,Answer>();

            List<Question> questions = new EditableList<Question>();
            var questionAssignment = _surveyQuestionRepository.GetAllList(a => a.SurveyId == surveyId);

            foreach (var surveyQuestion in questionAssignment)
            {
                var question = _questionRepository.Get(surveyQuestion.QuestionId);

                questions.Add(question);
            }


            foreach (var question in questions)
            {
                //Answer from the user 
                var answer =
                    _answerRepository.FirstOrDefault(a => a.QuestionId == question.Id &&
                                                          a.CreatorUserId == abpSessionUserId) ?? new Answer();

                result.Add(question,answer);
            }

            return result;
        }
    }
}

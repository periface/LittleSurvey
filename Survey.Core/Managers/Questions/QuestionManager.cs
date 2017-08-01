using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Survey.Core.Entities;

namespace Survey.Core.Managers.Questions
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionManager(IRepository<Question> questionRepository)
        {
            this._questionRepository = questionRepository;
        }

        public Task<int> CreateQuestionAsync(string text)
        {
            var question = new Question(text);
            return _questionRepository.InsertOrUpdateAndGetIdAsync(question);
        }

        public int CreateQuestion(string text)
        {
            var question = new Question(text);
            return _questionRepository.InsertOrUpdateAndGetId(question);
        }

        public Task EditQuestionAsync(int questionId, string text)
        {
            var question = _questionRepository.FirstOrDefault(a => a.Id == questionId);
            if (question == null) return Task.FromResult(0);
            question.QuestionText = text;
            return _questionRepository.UpdateAsync(question);
        }

        public void EditQuestion(int questionId, string text)
        {
            var question = _questionRepository.FirstOrDefault(a => a.Id == questionId);
            if (question == null) return;
            question.QuestionText = text;
            _questionRepository.Update(question);
        }

        public Task<int> RemoveQuestionAsync(int questionId, string text)
        {
            throw new NotImplementedException();
        }

        public int RemoveQuestion(int questionId, string text)
        {
            throw new NotImplementedException();
        }

        public Task SetSurveyAsync(int questionId, int surveyId)
        {
            throw new NotImplementedException();
        }

        public void SetSurvey(int questionId, int surveyId)
        {
            throw new NotImplementedException();
        }
    }
}

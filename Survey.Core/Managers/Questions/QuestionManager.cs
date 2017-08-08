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
        private readonly IRepository<SurveyQuestion> _suveryQuestionsRepository;
        private readonly IRepository<SurveyQuestionAnswer> _surveyQuestionAnswerRepository;
        private readonly IRepository<SelectedAnswer> _selectedAnswerRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<OfferedAnswer> _offeredAnswerRepository;
        public QuestionManager(IRepository<Question> questionRepository, IRepository<SurveyQuestion> suveryQuestionsRepository, IRepository<SurveyQuestionAnswer> surveyQuestionAnswerRepository, IRepository<SelectedAnswer> selectedAnswerRepository, IRepository<Answer> answerRepository, IRepository<OfferedAnswer> offeredAnswerRepository)
        {
            this._questionRepository = questionRepository;
            _suveryQuestionsRepository = suveryQuestionsRepository;
            _surveyQuestionAnswerRepository = surveyQuestionAnswerRepository;
            _selectedAnswerRepository = selectedAnswerRepository;
            _answerRepository = answerRepository;
            _offeredAnswerRepository = offeredAnswerRepository;
        }

        public Task<int> CreateQuestionAsync(string text, bool inputAllowMultipleAnswers)
        {
            var question = new Question(text){AllowMultipleAnswers = inputAllowMultipleAnswers};
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

        public async Task<List<Question>> GetQuestionsFromSurvey(int surveyId)
        {
            var questions = new List<Question>();
            var questionAssignments = await _suveryQuestionsRepository.GetAllListAsync(a => a.SurveyId == surveyId);

            foreach (var questionAssignment in questionAssignments)
            {
                var questionInfo = _questionRepository.FirstOrDefault(a => a.Id == questionAssignment.QuestionId);
                questions.Add(questionInfo);
            }
            return questions;
        }

        public async Task Answer(int surveyId, int questionId, int[] offeredAnswersId, string otherText, long? abpSessionUserId)
        {

            var found = _answerRepository.FirstOrDefault(a => a.SurveyId == surveyId && a.QuestionId == questionId &&
                                                  a.CreatorUserId == abpSessionUserId);

            if (found != null)
            {
                found.OtherText = otherText;
                await _answerRepository.InsertOrUpdateAndGetIdAsync(found);
                if (offeredAnswersId.Any())
                {
                    ClearAnswers(found.Id);
                    foreach (var i in offeredAnswersId)
                    {
                        await _selectedAnswerRepository.InsertOrUpdateAndGetIdAsync(new SelectedAnswer()
                        {
                            AnswerId = found.Id,
                            SelectedAnswerId = i
                        });
                    }
                }
            }
            else
            {
                var answer = new Answer(surveyId, questionId, otherText) { CreatorUserId = abpSessionUserId };
                await _answerRepository.InsertOrUpdateAndGetIdAsync(answer);
                if (offeredAnswersId.Any())
                {
                    foreach (var i in offeredAnswersId)
                    {
                        await _selectedAnswerRepository.InsertOrUpdateAndGetIdAsync(new SelectedAnswer()
                        {
                            AnswerId = answer.Id,
                            SelectedAnswerId = i
                        });
                    }
                }
            }
            
        }

        private void ClearAnswers(int foundId)
        {
            var all = _selectedAnswerRepository.GetAllList(a => a.AnswerId == foundId);
            foreach (var selectedAnswer in all)
            {
                _selectedAnswerRepository.Delete(selectedAnswer);
            }
        }

        public Task<int> CreatePredefinedAnswer(string txt)
        {
            return _offeredAnswerRepository.InsertOrUpdateAndGetIdAsync(new OfferedAnswer(txt));
        }

        public Task SetPredefinedAnswer(int surveyId, int questionId, int idPredefinedAnswer)
        {
            return _surveyQuestionAnswerRepository.InsertOrUpdateAndGetIdAsync(new SurveyQuestionAnswer(surveyId, questionId,
                 idPredefinedAnswer));
        }
    }
}

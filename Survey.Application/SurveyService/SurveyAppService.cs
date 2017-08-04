using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Survey.Application.SurveyService.Dto;
using Survey.Core.Entities;
using Survey.Core.Managers.Questions;
using Survey.Core.Managers.Surveys;

namespace Survey.Application.SurveyService
{
    public class SurveyAppService : ApplicationService,ISurveyAppService
    {
        private readonly IRepository<Core.Entities.Survey> _surveyRepository;
        private readonly ISurveyManager _surveyManager;
        private readonly IQuestionManager _questionManager;
        private readonly IRepository<Answer> _answerRepository;
        public SurveyAppService(ISurveyManager surveyManager, IQuestionManager questionManager, IRepository<Core.Entities.Survey> surveyRepository, IRepository<Answer> answerRepository)
        {
            _surveyManager = surveyManager;
            _questionManager = questionManager;
            _surveyRepository = surveyRepository;
            _answerRepository = answerRepository;
        }

        public async Task<int> CreateEditSurvey(SurveyInputDto input)
        {
            if (input.Id == 0)
            {
                Logger.Debug($"Creando encuesta...{input.Description}");
                return await _surveyManager.CreateSurveyAsync(input.MapTo<Core.Entities.Survey>());
            }
            Logger.Debug($"Editando encuesta...{input.Id}");
            var survey = _surveyRepository.Get(input.Id);

            var edited = input.MapTo(survey);

            await _surveyManager.EditSurveyAsync(edited);
            return input.Id;
        }
        public async Task<int> CreateQuestion(QuestionInputDto input)
        {
            return await _questionManager.CreateQuestionAsync(input.QuestionText,input.AllowMultipleAnswers);
        }
        public async Task AssignQuestionToSurvey(int surveyId, int questionId)
        {
            await _surveyManager.AddQuestionAsync(questionId,surveyId);
        }
        public async Task RemoveQuestionFromSurvey(int surveyId, int questionId)
        {
            await _surveyManager.RemoveQuestionAsync(questionId, surveyId);
        }
        public Task DeleteSurvey(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SurveyDto>> GetSurveys()
        {
            var surveys = await _surveyRepository.GetAllListAsync();
            return surveys.Select(a=>a.MapTo<SurveyDto>()).ToList();
        }

        public Task CreateQuestion(string questionText)
        {
            return Task.CompletedTask;
        }

        public async Task<SurveyForUserDto> GetSurvey(string url)
        {
            var survey = _surveyManager.GetSurveyFromUrl(url);

            var answerAndQuestionsForUser = _surveyManager.GetQuestionsWithAnswers(AbpSession.UserId,survey.Id);
            return new SurveyForUserDto()
            {
                Answers = BuildAnswers(answerAndQuestionsForUser),
                Description = survey.Description,
                EndDateTime = survey.EndDateTime,
                StartDateTime = survey.StartDateTime,
                Id = survey.Id
            };
        }

        public async Task<int> AddPredefinedAnswer(string txt)
        {
           return await _questionManager.CreatePredefinedAnswer(txt);
        }

        public async Task SetOfferedAnswer(int surveyId,int questionId, int idPredefinedAnswer)
        {
            await _questionManager.SetPredefinedAnswer(surveyId, questionId,idPredefinedAnswer);
        }

        private List<QuestionDto> BuildAnswers(IDictionary<QuestionWithOffered, Answer> answerAndQuestionsForUser)
        {
            var result = new List<QuestionDto>();

            foreach (var answer in answerAndQuestionsForUser)
            {
                result.Add(new QuestionDto()
                {
                    AllowMultipleAnswers = answer.Key.Question.AllowMultipleAnswers,
                    OtherText = answer.Value.OtherText,
                    Id = answer.Key.Question.Id,
                    QuestionText = answer.Key.Question.QuestionText,
                    QuestionType = answer.Key.Question.QuestionType,
                    OfferedAnswers = answer.Key.OfferedAnswers.Select(a=>a.MapTo<OfferedAnswerDto>()).ToList(),
                    OfferedAnswerIds = answer.Value.SelectedAnswers.Select(a=>a.Id).ToArray()
                });
            }

            return result;
        }

        public async Task AnswerQuestion(AnswerInputDto answer)
        {
            await _questionManager.Answer(answer.SurveyId, answer.QuestionId, answer.OfferedAnswerIds, answer.OtherText,AbpSession.UserId);
        }

        public QuestionDto GetQuestion(int surveyId, int questionId)
        {
            var questions = _surveyManager.GetQuestions(surveyId);

            var question = questions.FirstOrDefault(a => a.Id == questionId);

            var mapped = question.MapTo<QuestionDto>();

            var index = questions.IndexOf(question);


            var prevQuestion = TryToGetElementAt(questions,index - 1);
            var nextQuestion = TryToGetElementAt(questions, index + 1);

            if (prevQuestion != null)
            {
                mapped.PrevQuestion = prevQuestion.Id;
            }
            if (nextQuestion != null)
            {
                mapped.NextQuestion = nextQuestion.Id;
            }
            return mapped;
        }

        private Question TryToGetElementAt(List<Question> questions,int index)
        {
            try
            {
                var prevQuestion = questions.ElementAt(index);
                return prevQuestion;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task BulkAnswer(List<AnswerInputDto> answers)
        {
            foreach (AnswerInputDto answer in answers)
            {
                await _questionManager.Answer(answer.SurveyId, answer.QuestionId, answer.OfferedAnswerIds, answer.OtherText, AbpSession.UserId);
            }
        }
        public int GetAllAnswers()
        {
           return _answerRepository.Count();
        }
    }
}

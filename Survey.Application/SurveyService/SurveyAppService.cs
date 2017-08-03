using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Survey.Application.SurveyService.Dto;
using Survey.Core.Entities;
using Survey.Core.Managers.Questions;
using Survey.Core.Managers.Surveys;

namespace Survey.Application.SurveyService
{
    public class SurveyAppService : ISurveyAppService
    {
        private readonly IRepository<Core.Entities.Survey> _surveyRepository;
        private readonly ISurveyManager _surveyManager;
        private readonly IQuestionManager _questionManager;
        public IAbpSession AbpSession;
        public ILogger Logger;
        public SurveyAppService(ISurveyManager surveyManager, IQuestionManager questionManager, IRepository<Core.Entities.Survey> surveyRepository)
        {
            _surveyManager = surveyManager;
            _questionManager = questionManager;
            _surveyRepository = surveyRepository;
            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
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
            return await _questionManager.CreateQuestionAsync(input.QuestionText);
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

        public Task<List<SurveyDto>> GetSurveys()
        {
            throw new NotImplementedException();
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
                StartDateTime = survey.EndDateTime
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

        private List<QuestionDto> BuildAnswers(IDictionary<Question, Answer> answerAndQuestionsForUser)
        {
            var result = new List<QuestionDto>();

            foreach (var answer in answerAndQuestionsForUser)
            {
                result.Add(new QuestionDto()
                {
                    AllowMultipleAnswers = answer.Key.AllowMultipleAnswers,
                    OtherText = answer.Value.OtherText,
                    QuestionText = answer.Key.QuestionText,
                    QuestionType = answer.Key.QuestionType
                });
            }

            return result;
        }

        public async Task AnswerQuestion(AnswerInputDto answer)
        {
            await _questionManager.Answer(answer.SurveyId, answer.QuestionId, answer.OfferedAnswerIds, answer.OtherText,AbpSession.UserId);
        }
    }
}

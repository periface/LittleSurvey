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
    public class SurveyAppService : ApplicationService, ISurveyAppService
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
            return await _questionManager.CreateQuestionAsync(input.QuestionText, input.AllowMultipleAnswers);
        }
        public async Task AssignQuestionToSurvey(int surveyId, int questionId)
        {
            await _surveyManager.AddQuestionAsync(questionId, surveyId);
        }

        public async Task AssignQuestionsToSurvey(List<AssignQuestionInputDto> input, int surveyId)
        {
            await _surveyManager.AddQuestionsAsync(surveyId, input.Select(a => a.QuestionId).ToList());
        }

        public async Task RemoveQuestionFromSurvey(int surveyId, int questionId)
        {
            await _surveyManager.RemoveQuestionAsync(questionId, surveyId);
        }
        public async Task DeleteSurvey(int id)
        {
            var survey = await _surveyRepository.FirstOrDefaultAsync(a => a.Id == id);

            if (survey == null) return;

            await _surveyManager.DeleteSurveyAsync(survey);
        }

        public async Task<SurveyWithOnlyFirstQuestionForUserDto> GetSurveyFirstQuestion(string url)
        {
            var survey = _surveyManager.GetSurveyFromUrl(url);

            var answerAndQuestionsForUser = await _surveyManager.GetQuestionsWithAnswersAsync(AbpSession.UserId, survey.Id);
            return new SurveyWithOnlyFirstQuestionForUserDto()
            {
                Answer = BuildAnswer(answerAndQuestionsForUser),
                Description = survey.Description,
                EndDateTime = survey.EndDateTime,
                StartDateTime = survey.StartDateTime,
                Id = survey.Id
            };
        }

        private QuestionDto BuildAnswer(IDictionary<QuestionWithOffered, Answer> answerAndQuestionsForUser)
        {
            var answer = answerAndQuestionsForUser.First();

            var index = answerAndQuestionsForUser.Keys.ToList().IndexOf(answer.Key);

            var prevQuestion = TryToGetElementAt(answerAndQuestionsForUser.Keys.ToList(), index - 1);
            var nextQuestion = TryToGetElementAt(answerAndQuestionsForUser.Keys.ToList(), index + 1);

            if (prevQuestion == null) prevQuestion = new QuestionWithOffered();
            if (nextQuestion == null) nextQuestion = new QuestionWithOffered();

            var result = new QuestionDto()
            {
                AllowMultipleAnswers = answer.Key.Question.AllowMultipleAnswers,
                OtherText = answer.Value.OtherText,
                Id = answer.Key.Question.Id,
                QuestionText = answer.Key.Question.QuestionText,
                QuestionType = answer.Key.Question.QuestionType,
                OfferedAnswers = answer.Key.OfferedAnswers.Select(a => a.MapTo<OfferedAnswerDto>()).ToList(),
                OfferedAnswerIds = answer.Value.SelectedAnswers.Select(a => a.SelectedAnswerId).ToArray(),
                NextQuestion = nextQuestion.Question.Id,
                PrevQuestion = prevQuestion.Question.Id
            };

            return result;
        }

        public async Task<List<SurveyDto>> GetSurveys()
        {
            var surveys = await _surveyRepository.GetAllListAsync();
            return surveys.Select(a => a.MapTo<SurveyDto>()).ToList();
        }

        public Task CreateQuestion(string questionText)
        {
            return Task.CompletedTask;
        }

        public async Task<SurveyForUserDto> GetSurvey(string url)
        {
            var survey = _surveyManager.GetSurveyFromUrl(url);

            var answerAndQuestionsForUser = await _surveyManager.GetQuestionsWithAnswersAsync(AbpSession.UserId, survey.Id);
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

        public async Task SetOfferedAnswer(int surveyId, int questionId, int idPredefinedAnswer)
        {
            await _questionManager.SetPredefinedAnswer(surveyId, questionId, idPredefinedAnswer);
        }

        private List<QuestionDto> BuildAnswers(IDictionary<QuestionWithOffered, Answer> answerAndQuestionsForUser)
        {
            var result = new List<QuestionDto>();

            foreach (var answer in answerAndQuestionsForUser)
            {
                var index = answerAndQuestionsForUser.Keys.ToList().IndexOf(answer.Key);

                var prevQuestion = TryToGetElementAt(answerAndQuestionsForUser.Keys.ToList(), index - 1);
                var nextQuestion = TryToGetElementAt(answerAndQuestionsForUser.Keys.ToList(), index + 1);

                if (prevQuestion == null) prevQuestion = new QuestionWithOffered();
                if (nextQuestion == null) nextQuestion = new QuestionWithOffered();

                result.Add(new QuestionDto()
                {
                    AllowMultipleAnswers = answer.Key.Question.AllowMultipleAnswers,
                    OtherText = answer.Value.OtherText,
                    Id = answer.Key.Question.Id,
                    QuestionText = answer.Key.Question.QuestionText,
                    QuestionType = answer.Key.Question.QuestionType,
                    OfferedAnswers = answer.Key.OfferedAnswers.Select(a => a.MapTo<OfferedAnswerDto>()).ToList(),
                    OfferedAnswerIds = answer.Value.SelectedAnswers.Select(a => a.SelectedAnswerId).ToArray(),
                    NextQuestion = nextQuestion.Question.Id,
                    PrevQuestion = prevQuestion.Question.Id
                });
            }

            return result;
        }

        private QuestionWithOffered TryToGetElementAt(List<QuestionWithOffered> toList, int index)
        {
            try
            {
                var prevQuestion = toList.ElementAt(index);
                return prevQuestion;
            }
            catch (Exception)
            {
                return new QuestionWithOffered();
            }
        }

        public async Task AnswerQuestion(AnswerInputDto answer)
        {
            await _questionManager.Answer(answer.SurveyId, answer.QuestionId, answer.OfferedAnswerIds, answer.OtherText, AbpSession.UserId);
        }

        public QuestionDto GetQuestion(int surveyId, int questionId)
        {
            var questions = _surveyManager.GetQuestions(surveyId);

            var question = questions.FirstOrDefault(a => a.Id == questionId);

            var mapped = question.MapTo<QuestionDto>();

            var index = questions.IndexOf(question);


            var prevQuestion = TryToGetElementAt(questions, index - 1);
            var nextQuestion = TryToGetElementAt(questions, index + 1);

            if (prevQuestion != null)
            {
                mapped.PrevQuestion = prevQuestion.Id;
            }
            if (nextQuestion != null)
            {
                mapped.NextQuestion = nextQuestion.Id;
            }
            mapped.SurveyId = surveyId;

            var answers = _surveyManager.GetQuestionsWithAnswers(AbpSession.UserId, surveyId);
            var questionWAnswer = answers.Keys.FirstOrDefault(a => a.Question.Id == questionId);
            var response = answers.Values.FirstOrDefault(a => a.QuestionId == questionId && a.SurveyId == surveyId);
            if (questionWAnswer != null)
            {
                mapped.OfferedAnswers = questionWAnswer.OfferedAnswers.Select(a => a.MapTo<OfferedAnswerDto>())
                    .ToList();
            }

            if (response != null)
                mapped.OfferedAnswerIds = response.SelectedAnswers.Select(a => a.SelectedAnswerId).ToArray();

            if (response != null) mapped.OtherText = response.OtherText;
            return mapped;
        }

        private Question TryToGetElementAt(IEnumerable<Question> questions, int index)
        {
            try
            {
                var prevQuestion = questions.ElementAt(index);
                return prevQuestion;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task BulkAnswer(List<AnswerInputDto> answers)
        {
            foreach (var answer in answers)
            {
                await _questionManager.Answer(answer.SurveyId, answer.QuestionId, answer.OfferedAnswerIds, answer.OtherText, AbpSession.UserId);
            }
        }

        public List<QuestionDto> GetAvailableQuestions(int id)
        {
            List<Question> questions = _questionManager.GetQuestionsForSurveyAsync(id);

            return questions.Select(a => a.MapTo<QuestionDto>()).ToList();
        }

        public async Task<SurveyDto> GetSurveyById(int id)
        {
            var survey = await _surveyRepository.FirstOrDefaultAsync(a => a.Id == id);
            return survey.MapTo<SurveyDto>();
        }

        public List<QuestionDto> GetAssignedQuestions(int id)
        {
            List<Question> questions = _questionManager.GetQuestionsForSurveyInvertedAsync(id);

            return questions.Select(a => a.MapTo<QuestionDto>()).ToList();
        }

        public int GetAllAnswers()
        {
            return _answerRepository.Count();
        }
    }
}

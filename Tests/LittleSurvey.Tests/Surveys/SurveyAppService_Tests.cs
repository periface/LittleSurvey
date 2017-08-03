using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Survey.Application.SurveyService;
using Survey.Application.SurveyService.Dto;
using Survey.Core.Entities;
using Xunit;

namespace LittleSurvey.Tests.Surveys
{
    public class SurveyAppService_Tests : LittleSurveyTestBase
    {
        private readonly ISurveyAppService _surveyAppService;

        public SurveyAppService_Tests()
        {
            _surveyAppService = Resolve<ISurveyAppService>();
        }

        [Fact]
        public async Task CreateSurveyTest()
        {
            await CreateFakeSurvey("Mi encuesta");
            await UsingDbContextAsync(async context =>
            {
                var survey = await context.Surveys.FirstOrDefaultAsync(a => a.Description == "Mi encuesta");
                survey.ShouldNotBeNull();
            });
        }
        [Fact]
        public async Task CreateAndSetQuestion()
        {
            LoginAsDefaultTenantAdmin();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurvey("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?");
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();
            });
        }
        [Fact]
        public async Task CreateOfferedAnswersAndSet_Test()
        {
            LoginAsDefaultTenantAdmin();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurvey("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?");
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();

                /*Survey pre-defined-answers*/

                int idBueno = await _surveyAppService.AddPredefinedAnswer("Bueno");

                idBueno.ShouldNotBe(0);

                int idMalo = await _surveyAppService.AddPredefinedAnswer("Malo");

                idMalo.ShouldNotBe(0);
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idBueno);
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idMalo);

                /*-----Survey pre-defined-answers*/
            });
        }
        [Fact]
        public async Task SimulateAnswering()
        {
            LoginAsDefaultTenantAdmin();

            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurvey("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?");
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();

                /*Survey pre-defined-answers*/
                int idBueno = await _surveyAppService.AddPredefinedAnswer("Bueno");

                idBueno.ShouldNotBe(0);

                int idMalo = await _surveyAppService.AddPredefinedAnswer("Malo");

                idMalo.ShouldNotBe(0);
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idBueno);
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idMalo);
                /*-----Survey pre-defined-answers*/

                /*Survey answers*/

                var survey = await _surveyAppService.GetSurvey(guid);
                await MockAnswers(survey);
                var answeredSurvey = await _surveyAppService.GetSurvey(guid);

                answeredSurvey.Answers.Count.ShouldNotBe(0);

                foreach (var answeredSurveyAnswer in answeredSurvey.Answers)
                {
                    answeredSurveyAnswer.OfferedAnswers.Count.ShouldNotBe(0);

                    answeredSurveyAnswer.IsAnswered.ShouldBe(true);

                    answeredSurveyAnswer.OfferedAnswerIds.Length.ShouldNotBe(0);
                }

                /*-----Survey answers*/
            });
        }

        private async Task MockAnswers(SurveyForUserDto survey)
        {
            foreach (var surveyAnswer in survey.Answers)
            {
                int randomAnswer = GetRandomAnswer(surveyAnswer.OfferedAnswers);
                await _surveyAppService.AnswerQuestion(new AnswerInputDto()
                {
                    OfferedAnswerIds = new[] { randomAnswer },
                    OtherText = string.Empty,
                    //Id holds the questionId
                    QuestionId = surveyAnswer.Id,
                    SurveyId = survey.Id
                });
            }
        }

        private int GetRandomAnswer(List<OfferedAnswerDto> surveyAnswerOfferedAnswers)
        {
            var offeredAnswerDto = surveyAnswerOfferedAnswers.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
            if (offeredAnswerDto != null)
                return offeredAnswerDto.Id;
            return 0;
        }

        [Fact]
        public async Task CreateSetQuestionAndRemove()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurvey("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?");
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();
                await _surveyAppService.RemoveQuestionFromSurvey(surveyId, questionId);

                questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldBeNull();
            });
        }
        private async Task<int> CreateFakeQuestion(string txt)
        {
            return await _surveyAppService.CreateQuestion(new QuestionInputDto()
            {
                QuestionText = txt
            });
        }

        private async Task<int> CreateFakeSurvey(string name, string url = "")
        {
            return await _surveyAppService.CreateEditSurvey(new SurveyInputDto()
            {
                Description = name,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddDays(5),
                SurveyUrl = url
            });
        }
    }
}

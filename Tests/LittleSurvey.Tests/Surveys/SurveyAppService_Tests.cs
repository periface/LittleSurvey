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
        /// <summary>
        /// Simula la creación de una encuesta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateSurvey_Test()
        {
            await CreateFakeSurveyObject("Mi encuesta");
            await UsingDbContextAsync(async context =>
            {
                var survey = await context.Surveys.FirstOrDefaultAsync(a => a.Description == "Mi encuesta");
                survey.ShouldNotBeNull();
            });
        }
        /// <summary>
        /// Simula la creación de una encuesta y agrega una pregunta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAndSetQuestion_Test()
        {
            LoginAsDefaultTenantAdmin();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurveyObject("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            //Crea una pregunta
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?", true);
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                //Asigna la pregunta a la encuesta
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();
            });
        }
        /// <summary>
        /// Simula la creación de una encuesta, agrega una pregunta y agrega respuestas predefinidas a la pregunta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOfferedAnswersAndSet_Test()
        {
            LoginAsDefaultTenantAdmin();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurveyObject("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?", true);
            questionId.ShouldNotBe(0);
            await UsingDbContextAsync(async context =>
            {
                var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                question.ShouldNotBeNull();
                await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                questionAssignment.ShouldNotBeNull();

                /*Crea las respuestas predefinidas*/

                int idBueno = await _surveyAppService.AddPredefinedAnswer("Bueno");

                idBueno.ShouldNotBe(0);

                int idMalo = await _surveyAppService.AddPredefinedAnswer("Malo");

                idMalo.ShouldNotBe(0);

                /*Asigna las respuestas predefinidas a la pregunta*/
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idBueno);
                await _surveyAppService.SetOfferedAnswer(surveyId, question.Id, idMalo);

            });
        }

        /// <summary>
        /// Simula la creación de una encuesta completa y despues simula a un usuario contestando la encuesta
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SimulateAnswering_Test()
        {

            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            await CreateCompleteSurvey(guid);
            var survey = await _surveyAppService.GetSurvey(guid);
            await MockAnswers(survey); //-- Simula el llenado de la encuesta

            //Cargamos de nuevo la encuesta, pero ahora esta tendra las respuestas
            var answeredSurvey = await _surveyAppService.GetSurvey(guid);

            answeredSurvey.Answers.Count.ShouldNotBe(0);
            answeredSurvey.Answers.Count.ShouldBe(2);

            foreach (var answeredSurveyAnswer in answeredSurvey.Answers)
            {
                answeredSurveyAnswer.OfferedAnswers.Count.ShouldNotBe(0);

                answeredSurveyAnswer.IsAnswered.ShouldBe(true);

                //Aqui se almacenan los id´s de las respuestas predefinidas seleccionadas por el cliente
                answeredSurveyAnswer.OfferedAnswerIds.Length.ShouldNotBe(0);
            }

            /*-----Survey answers*/
        }
        [Fact]
        public async Task CreateSetQuestionAndRemove_Test()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            var surveyId = await CreateFakeSurveyObject("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            var questionId = await CreateFakeQuestion("¿Como califica el servicio?", true);
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

        [Fact]
        public async Task PrevNextSearch_Test()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 6);
            await CreateCompleteSurvey(guid,new []{ "Q1", "Q2", "Q3", "Q4" });
            var survey = await _surveyAppService.GetSurvey(guid);
            var firstOrDefault = survey.Answers.FirstOrDefault(a=>a.QuestionText == "Q3");
            if (firstOrDefault != null)
            {
                var check = _surveyAppService.GetQuestion(survey.Id, firstOrDefault.Id);
                check.NextQuestion.ShouldBe(4);
                check.PrevQuestion.ShouldBe(2);


                var secondCheck = _surveyAppService.GetQuestion(survey.Id,check.NextQuestion);
                secondCheck.NextQuestion.ShouldBe(0);
                secondCheck.PrevQuestion.ShouldBe(3);
            }
        }
        private async Task<int> CreateFakeQuestion(string txt, bool b)
        {
            return await _surveyAppService.CreateQuestion(new QuestionInputDto()
            {
                QuestionText = txt,
                AllowMultipleAnswers = b
            });
        }

        private async Task<int> CreateFakeSurveyObject(string name, string url = "")
        {
            return await _surveyAppService.CreateEditSurvey(new SurveyInputDto()
            {
                Description = name,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddDays(5),
                SurveyUrl = url
            });
        }
        private int GetRandomAnswer(List<OfferedAnswerDto> surveyAnswerOfferedAnswers)
        {
            var offeredAnswerDto = surveyAnswerOfferedAnswers.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
            if (offeredAnswerDto != null)
                return offeredAnswerDto.Id;
            return 0;
        }
        private async Task MockAnswers(SurveyForUserDto survey)
        {
            foreach (var surveyAnswer in survey.Answers)
            {

                if (surveyAnswer.AllowMultipleAnswers)
                {
                    IEnumerable<int> randoms = GetRandomAnswers(surveyAnswer.OfferedAnswers);
                    //Responde la pregunta
                    await _surveyAppService.AnswerQuestion(new AnswerInputDto()
                    {
                        OfferedAnswerIds = randoms.ToArray(),
                        OtherText = string.Empty,
                        //Id holds the questionId
                        QuestionId = surveyAnswer.Id,
                        SurveyId = survey.Id
                    });
                }
                else
                {
                    //Obtiene una respuesta aleatoria de las respuestas ofrecidas
                    int randomAnswer = GetRandomAnswer(surveyAnswer.OfferedAnswers);
                    //Responde la pregunta
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
        }

        private IEnumerable<int> GetRandomAnswers(List<OfferedAnswerDto> surveyAnswerOfferedAnswers)
        {
            var prevIds = new List<int>();
            foreach (var surveyAnswerOfferedAnswer in surveyAnswerOfferedAnswers)
            {
                var offeredAnswerDto = !prevIds.Any() ? surveyAnswerOfferedAnswers.OrderBy(a => Guid.NewGuid()).FirstOrDefault() : surveyAnswerOfferedAnswers.Where(a => !prevIds.Contains(a.Id)).OrderBy(a => Guid.NewGuid()).FirstOrDefault();
                if (offeredAnswerDto != null)
                {
                    prevIds.Add(offeredAnswerDto.Id);
                    yield return offeredAnswerDto.Id;
                }
            }
        }

        private async Task CreateCompleteSurvey(string guid, string[] questions = null, string[] possibleAnswers = null)
        {
            if (questions == null)
            {
                questions = new[] { "¿Como califica el servicio?", "¿Como califica el servicio del vendedor" };
            }
            if (possibleAnswers == null)
            {
                possibleAnswers = new[] { "Excelente", "Bueno", "Malo", "Pesimo" };
            }
            LoginAsDefaultTenantAdmin();
            var ids = new List<int>();
            var surveyId = await CreateFakeSurveyObject("Mi encuesta", guid);
            surveyId.ShouldNotBe(0);
            //For switch true or false
            var cond = false;
            foreach (var question in questions)
            {
                var questionId = await CreateFakeQuestion(question, cond);
                questionId.ShouldNotBe(0);
                ids.Add(questionId);
                cond = !cond;
            }
            await UsingDbContextAsync(async context =>
            {
                /*Crea las respuestas predefinidas*/
                var idspa = new List<int>();
                foreach (var possibleAnswer in possibleAnswers)
                {
                    var idAnswer = await _surveyAppService.AddPredefinedAnswer(possibleAnswer);
                    idAnswer.ShouldNotBe(0);
                    idspa.Add(idAnswer);
                }
                foreach (var questionId in ids)
                {
                    var question = await context.Questions.FirstOrDefaultAsync(a => a.Id == questionId);
                    question.ShouldNotBeNull();
                    await _surveyAppService.AssignQuestionToSurvey(surveyId, questionId);
                    var questionAssignment = await context.SurveyQuestions.FirstOrDefaultAsync(a => a.QuestionId == questionId && a.SurveyId == surveyId);
                    questionAssignment.ShouldNotBeNull();

                    /*Asigna las respuestas predefinidas a la pregunta*/

                    foreach (var i in idspa)
                    {
                        await _surveyAppService.SetOfferedAnswer(surveyId, questionId, i);
                    }
                }
            });
        }
    }
}

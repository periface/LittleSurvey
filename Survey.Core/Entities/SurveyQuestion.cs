using Abp.Domain.Entities;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Relationship between the survey and the question
    /// <para>Survey can have many questions and a question can be used in many surveys</para>
    /// </summary>
    public class SurveyQuestion : Entity
    {
        public SurveyQuestion()
        {
            
        }
        /// <summary>
        /// For convenience only
        /// </summary>
        /// <param name="survey"></param>
        /// <param name="question"></param>
        public SurveyQuestion(Survey survey,Question question)
        {
            SurveyId = survey.Id;
            QuestionId = question.Id;
        }
        /// <summary>
        /// For convenience only
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="questionId"></param>
        public SurveyQuestion(int surveyId, int questionId)
        {
            SurveyId = surveyId;
            QuestionId = questionId;
        }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
    }
}
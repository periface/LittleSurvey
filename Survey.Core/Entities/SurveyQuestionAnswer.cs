using Abp.Domain.Entities;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Relationship between the survey, the question and the offered answer
    /// <para>Survey can have many questions and a question can be used in many surveys</para>
    /// </summary>
    public class SurveyQuestionAnswer : Entity
    {
        /// <summary>
        /// For convenience only
        /// </summary>
        public SurveyQuestionAnswer(int surveyId,int questionId,int offeredAnswerId)
        {
            SurveyId = surveyId;
            QuestionId = questionId;
            OfferedAnswerId = offeredAnswerId;
        }

        public SurveyQuestionAnswer()
        {

        }
        /// <summary>
        /// For convenience only
        /// </summary>
        public SurveyQuestionAnswer(Survey survey,Question question,OfferedAnswer offeredAnswer)
        {
            SurveyId = survey.Id;
            QuestionId = question.Id;
            OfferedAnswerId = offeredAnswer.Id;
        }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int OfferedAnswerId { get; set; }
    }
}
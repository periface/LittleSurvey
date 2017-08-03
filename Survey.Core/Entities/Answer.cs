using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Holds the answer input from the user
    /// </summary>
    public class Answer : FullAuditedEntity
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int OfferedAnswerId { get; set; }
        public string OtherText { get; set; }
        public ICollection<SelectedAnswer> SelectedAnswers { get; set; }
        public Answer()
        {
            
        }
        /// <summary>
        /// For convenience only
        /// </summary>
        public Answer(int surveyId,int questionId,string otherText)
        {
            SurveyId = surveyId;
            QuestionId = questionId;
            OtherText = otherText;
        }
        /// <summary>
        /// For convenience only
        /// </summary>
        public Answer(Survey survey,Question question,OfferedAnswer offeredAnswer,string otherText)
        {
            SurveyId = survey.Id;
            QuestionId = question.Id;
            OfferedAnswerId = offeredAnswer.Id;
            OtherText = otherText;
        }
    }
}
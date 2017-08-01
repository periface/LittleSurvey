using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Holds the survey info
    /// </summary>
    public class Survey : FullAuditedEntity
    {
        public Survey()
        {
            
        }
        /// <summary>
        /// Creates a new survey instance (only for convenience)
        /// </summary>
        /// <param name="description"></param>
        /// <param name="daysFromNow"></param>
        public Survey(string description,int daysFromNow)
        {
            Description = description;
            StartDateTime = DateTime.Now;
            EndDateTime = StartDateTime.AddDays(daysFromNow);
        }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
    /// <summary>
    /// Holds the question text
    /// </summary>
    public class Question : FullAuditedEntity
    {
        public Question()
        {
            
        }

        public Question(string text)
        {
            QuestionText = text;
        }
        public string QuestionText { get; set; }
    }
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
    /// <summary>
    /// Holds an offered answer for the survey
    /// </summary>
    public class OfferedAnswer : Entity
    {
        public OfferedAnswer()
        {
            
        }

        /// <summary>
        /// For convenience only
        /// </summary>
        /// <param name="text"></param>
        public OfferedAnswer(string text)
        {
            AnswerText = text;
        }
        public string AnswerText { get; set; } 
    }
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
    /// <summary>
    /// Holds the answer input from the user
    /// </summary>
    public class Answer : FullAuditedEntity
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int OfferedAnswerId { get; set; }
        public string OtherText { get; set; }

        public Answer()
        {
            
        }
        /// <summary>
        /// For convenience only
        /// </summary>
        public Answer(int surveyId,int questionId,int offeredAnswerId,string otherText)
        {
            SurveyId = surveyId;
            QuestionId = questionId;
            OfferedAnswerId = offeredAnswerId;
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

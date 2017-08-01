using Abp.Domain.Entities;

namespace Survey.Core.Entities
{
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
}
using Abp.Domain.Entities.Auditing;

namespace Survey.Core.Entities
{
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
        public int QuestionType { get; set; }
        public int Order { get; set; }
    }
}
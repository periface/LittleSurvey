using System.Collections.Generic;

namespace Survey.Core.Entities
{
    public class QuestionWithOffered
    {
        public Question Question { get; set; } = new Question();
        public List<OfferedAnswer> OfferedAnswers { get; set; }
    }
}
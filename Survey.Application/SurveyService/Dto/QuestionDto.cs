using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Castle.Core.Internal;
using Survey.Core.Entities;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(Question))]
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public int Order { get; set; }
        public bool AllowMultipleAnswers { get; set; }
        //For multiple selection check
        public int[] OfferedAnswerIds { get; set; }
        public List<OfferedAnswerDto> OfferedAnswers { get; set; } = new List<OfferedAnswerDto>();
        public string OtherText { get; set; }
        public bool IsAnswered
        {
            get
            {
                if (!OfferedAnswerIds.Any())
                {
                    if (OtherText.IsNullOrEmpty()) return false;
                    return true;
                }
                return true;
            }
        }
    }
}

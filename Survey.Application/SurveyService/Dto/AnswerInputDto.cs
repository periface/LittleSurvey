using Abp.AutoMapper;
using Survey.Core.Entities;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(Answer))]
    public class AnswerInputDto
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int[] OfferedAnswerIds { get; set; }
        public string OtherText { get; set; }
    }
}
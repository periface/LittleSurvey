using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Survey.Core.Entities;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(OfferedAnswer))]
    public class OfferedAnswerDto : EntityDto
    {
        public string AnswerText { get; set; }
    }
}
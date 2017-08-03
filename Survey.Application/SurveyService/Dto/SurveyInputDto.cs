using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(Core.Entities.Survey))]
    public class SurveyInputDto : EntityDto
    {
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SurveyUrl { get; set; }
    }
}

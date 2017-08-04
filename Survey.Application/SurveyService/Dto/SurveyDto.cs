using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(Core.Entities.Survey))]
    public class SurveyDto
    {
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SurveyUrl { get; set; }
    }
}

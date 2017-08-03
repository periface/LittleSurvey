using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Survey.Core.Entities;

namespace Survey.Application.SurveyService.Dto
{
    [AutoMap(typeof(Question))]
    public class QuestionInputDto
    {
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public int Order { get; set; }
        public bool AllowMultipleAnswers { get; set; }
    }
}

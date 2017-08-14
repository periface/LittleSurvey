using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Application.SurveyService.Dto
{
    public class AssignQuestionInputDto
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
    }
}

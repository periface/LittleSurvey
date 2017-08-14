using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Application.SurveyService.Dto
{
    public class SetQuestionsDto
    {
        public SetQuestionsDto(List<QuestionDto> availableQuestions, List<QuestionDto> assignedQuestions, SurveyDto survey)
        {
            AvailableQuestions = availableQuestions;
            Survey = survey;
            AssignedQuestions = assignedQuestions;
        }

        private SetQuestionsDto()
        {
            
        }
        public List<QuestionDto> AvailableQuestions { get; set; } = new List<QuestionDto>();
        public List<QuestionDto> AssignedQuestions { get; set; } = new List<QuestionDto>();
        public SurveyDto Survey { get; set; } = new SurveyDto();
    }
}

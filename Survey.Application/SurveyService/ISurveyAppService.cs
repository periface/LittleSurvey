using Abp.Application.Services;
using System.Threading.Tasks;

namespace Survey.Application.SurveyService
{
    public interface ISurveyAppService : IApplicationService
    {
        Task CreateSurvey();
    }
}

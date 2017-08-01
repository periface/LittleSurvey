using System.Threading.Tasks;
using Abp.Application.Services;
using LittleSurvey.Roles.Dto;

namespace LittleSurvey.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}

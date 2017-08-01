using Abp.Authorization;
using LittleSurvey.Authorization.Roles;
using LittleSurvey.MultiTenancy;
using LittleSurvey.Users;

namespace LittleSurvey.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

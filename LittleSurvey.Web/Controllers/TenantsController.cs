using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using LittleSurvey.Authorization;
using LittleSurvey.MultiTenancy;

namespace LittleSurvey.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : LittleSurveyControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}
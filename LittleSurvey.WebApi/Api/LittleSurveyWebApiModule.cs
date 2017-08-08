using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Survey.Application;

namespace LittleSurvey.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(LittleSurveyApplicationModule), typeof(SurveyApplicationModule))]
    public class LittleSurveyWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(LittleSurveyApplicationModule).Assembly, "app")
                .Build();
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(SurveyApplicationModule).Assembly, "app")
                .Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}

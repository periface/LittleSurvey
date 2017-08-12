using System.Reflection;
using Abp.Modules;
using Abp.Resources.Embedded;
using Survey.Application;

namespace Survey.Web
{
    [DependsOn(typeof(SurveyApplicationModule))]
    public class SurveyWebModule : AbpModule
    {
        public override void Initialize()
        {
            var nameSpace = typeof(SurveyWebModule);
            var fullNmspc = nameSpace.Namespace;
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.EmbeddedResources.Sources.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    Assembly.GetExecutingAssembly(),
                    "Survey.Web.Views"
                )
            );
        }
    }
}
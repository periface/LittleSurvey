using System.Reflection;
using Abp.Modules;
using Survey.Core;

namespace Survey.Application
{
    [DependsOn(typeof(SurveyCoreModule))]
    public class SurveyApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

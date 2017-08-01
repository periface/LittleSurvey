using System.Reflection;
using Abp.Modules;

namespace Survey.Core
{
    public class SurveyCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

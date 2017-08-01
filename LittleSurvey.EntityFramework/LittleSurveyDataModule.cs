using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using LittleSurvey.EntityFramework;

namespace LittleSurvey
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(LittleSurveyCoreModule))]
    public class LittleSurveyDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LittleSurveyDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

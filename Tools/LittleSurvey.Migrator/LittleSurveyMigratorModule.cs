using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using LittleSurvey.EntityFramework;

namespace LittleSurvey.Migrator
{
    [DependsOn(typeof(LittleSurveyDataModule))]
    public class LittleSurveyMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<LittleSurveyDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
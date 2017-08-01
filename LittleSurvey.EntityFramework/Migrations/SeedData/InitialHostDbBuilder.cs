using LittleSurvey.EntityFramework;
using EntityFramework.DynamicFilters;

namespace LittleSurvey.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly LittleSurveyDbContext _context;

        public InitialHostDbBuilder(LittleSurveyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}

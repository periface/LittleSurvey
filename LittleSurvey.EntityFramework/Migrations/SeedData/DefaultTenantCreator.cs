using System.Linq;
using LittleSurvey.EntityFramework;
using LittleSurvey.MultiTenancy;

namespace LittleSurvey.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly LittleSurveyDbContext _context;

        public DefaultTenantCreator(LittleSurveyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}

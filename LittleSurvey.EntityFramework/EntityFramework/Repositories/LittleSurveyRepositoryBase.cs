using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace LittleSurvey.EntityFramework.Repositories
{
    public abstract class LittleSurveyRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<LittleSurveyDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected LittleSurveyRepositoryBase(IDbContextProvider<LittleSurveyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class LittleSurveyRepositoryBase<TEntity> : LittleSurveyRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected LittleSurveyRepositoryBase(IDbContextProvider<LittleSurveyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}

using UC.CSP.MeetingCenter.DAL;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected IDatabaseContext Context => DatabaseContextFactory.GetContext();

        public abstract TEntity GetById(int id);
        public abstract void Create(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract void Delete(TEntity entity);
    }
}
using System.Data.Entity;
using UC.CSP.MeetingCenter.DAL;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected AppDbContext Context => AppUnitOfWorkProvider.Instance.GetCurrent().Context;

        public abstract TEntity GetById(int id);
        public abstract void Create(TEntity entity);

        public virtual void Update(TEntity entity)
        {
            var e = GetById(entity.Id);
            if (e == null)
            {
                return;
            }

            Context.Entry(e).CurrentValues.SetValues(entity);
        }
        public abstract void Delete(TEntity entity);
    }
}
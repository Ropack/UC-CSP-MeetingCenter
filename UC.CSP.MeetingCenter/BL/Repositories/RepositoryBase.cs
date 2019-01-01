using System.Data.Entity;
using System.Linq;
using UC.CSP.MeetingCenter.DAL;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected AppDbContext Context => AppUnitOfWorkProvider.Instance.GetCurrent().Context;

        public virtual TEntity GetById(int id)
        {
            return Context.Set<TEntity>().FirstOrDefault(r => r.Id == id);
        }

        public virtual void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var e = GetById(entity.Id);
            if (e == null)
            {
                return;
            }

            Context.Entry(e).CurrentValues.SetValues(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
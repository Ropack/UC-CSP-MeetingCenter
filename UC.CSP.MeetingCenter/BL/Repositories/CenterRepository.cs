using System;
using System.Data;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class CenterRepository : RepositoryBase<Center>
    {
        public override Center GetById(int id)
        {
            var center = Context.Centers.FirstOrDefault(r => r.Id == id);
            
            return center;
        }

        public Center GetByCode(string code)
        {
            return Context.Centers.FirstOrDefault(r => r.Code == code);
        }

        public override void Create(Center entity)
        {
            VerifyConstraints(entity);
            Context.Centers.Add(entity);
        }

        public override void Delete(Center entity)
        {
            Context.Centers.Remove(entity);
        }

        private void VerifyConstraints(Center entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

    }
}
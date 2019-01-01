using System;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class AccessoryRepository : RepositoryBase<Accessory>
    {
        public override Accessory GetById(int id)
        {
            var accessory = Context.Accessories.FirstOrDefault(r => r.Id == id);
            return accessory;
        }

        public override void Create(Accessory entity)
        {
            VerifyConstraints(entity);
            Context.Accessories.Add(entity);
        }

        public override void Delete(Accessory entity)
        {
            Context.Accessories.Remove(entity);
        }

        private void VerifyConstraints(Accessory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }
    }
}
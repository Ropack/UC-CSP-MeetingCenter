using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class RoomRepository : RepositoryBase<Room>
    {
        public override Room GetById(int id)
        {
            var entity = Context.Rooms.Include(r => r.Reservations).FirstOrDefault(r => r.Id == id);
            return entity;
        }

        public Room GetByCode(string code)
        {
            var entity = Context.Rooms.Include(r => r.Reservations).FirstOrDefault(r => r.Code == code);

            return entity;
        }

        public override void Create(Room entity)
        {
            VerifyConstraints(entity);
            
            Context.Rooms.Add(entity);
        }

        public override void Delete(Room entity)
        {
            Context.Rooms.Remove(entity);
        }

        private void VerifyConstraints(Room entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }
        
    }
}
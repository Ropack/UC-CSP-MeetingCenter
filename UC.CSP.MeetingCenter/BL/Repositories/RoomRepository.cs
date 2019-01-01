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
    }
}
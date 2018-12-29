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
            if (center != null)
            {
                //center.Rooms = Context.Rooms.Where(r => r.CenterCode == center.Code).ToList();
                //// this is definitely bad, but we don't use database and have only few data
                //// entity framework would take care of that
                //foreach (var room in center.Rooms)
                //{
                //    room.Reservations = Context.Reservations.Where(r => r.MeetingRoomId == room.Id).ToList();
                //    foreach (var reservation in room.Reservations)
                //    {
                //        reservation.MeetingRoom = room;
                //    }
                //}
            }
            
            return center;
        }

        public Center GetByCode(string code)
        {
            return Context.Centers.FirstOrDefault(r => r.Code == code);
        }

        public override void Create(Center entity)
        {
            VerifyConstraints(entity);

            Context.NoteChange();
            entity.Id = Context.Centers.Any() ? Context.Centers.Max(r => r.Id) + 1 : 1;
            Context.Centers.Add(entity);
        }

        public override void Update(Center entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (Context.Centers.Count(r => r.Code == entity.Code) > 1)
            {
                throw new ConstraintException("Center code must be unique.");
            }
            var oldEntity = GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new ArgumentException("Updated center does not exists.");
            }

            Context.NoteChange();
            Map(entity, oldEntity);
        }

        public override void Delete(Center entity)
        {
            Context.NoteChange();
            Context.Centers.Remove(entity);
        }

        private void VerifyConstraints(Center entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (Context.Centers.Any(r => r.Code == entity.Code))
            {
                throw new ConstraintException("Center code must be unique.");
            }
        }

        private void Map(Center changedEntity, Center oldEntity)
        {
            oldEntity.Code = changedEntity.Code;
            oldEntity.Description = changedEntity.Description;
            oldEntity.Name = changedEntity.Name;
        }
    }
}
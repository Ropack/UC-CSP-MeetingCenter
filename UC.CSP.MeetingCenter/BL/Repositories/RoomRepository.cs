using System;
using System.Data;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class RoomRepository : RepositoryBase<Room>
    {
        public override Room GetById(int id)
        {
            var entity = Context.Rooms.FirstOrDefault(r => r.Id == id);
            if (entity != null)
            {
                //// this is definitely bad, but we don't use database and have only few data
                //// entity framework would take care of that
                //entity.Center = Context.Centers.Single(r => r.Code == entity.CenterCode);
                //entity.Reservations = Context.Reservations.Where(r => r.MeetingRoomId == entity.Id).ToList();
                //foreach (var reservation in entity.Reservations)
                //{
                //    reservation.MeetingRoom = entity;
                //}
            }

            return entity;
        }

        public Room GetByCode(string code)
        {
            var entity = Context.Rooms.FirstOrDefault(r => r.Code == code);
            if (entity != null)
            {
                entity.Center = Context.Centers.Single(r => r.Code == entity.CenterCode); 
                entity.Reservations = Context.Reservations.Where(r => r.MeetingRoomId == entity.Id).ToList();
                foreach (var reservation in entity.Reservations)
                {
                    reservation.MeetingRoom = entity;
                }
            }

            return entity;
        }

        public override void Create(Room entity)
        {
            VerifyConstraints(entity);

            Context.NoteChange();
            entity.Id = Context.Rooms.Any() ? Context.Rooms.Max(r => r.Id) + 1 : 1;
            Context.Centers.Single(c => c.Code == entity.CenterCode).Rooms.Add(entity);
            Context.Rooms.Add(entity);
        }

        public override void Update(Room entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (Context.Rooms.Count(r => r.Code == entity.Code) > 1)
            {
                throw new ConstraintException("Room code must be unique.");
            }
            var oldEntity = GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new ArgumentException("Updated room does not exists.");
            }

            Context.NoteChange();
            Map(entity, oldEntity);
        }

        public override void Delete(Room entity)
        {
            Context.NoteChange();
            Context.Rooms.Remove(entity);
        }

        private void VerifyConstraints(Room entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (Context.Rooms.Any(r => r.Code == entity.Code))
            {
                throw new ConstraintException("Room code must be unique.");
            }
        }

        private void Map(Room changedEntity, Room oldEntity)
        {
            oldEntity.Code = changedEntity.Code;
            oldEntity.Capacity = changedEntity.Capacity;
            oldEntity.CenterCode = changedEntity.CenterCode;
            oldEntity.Description = changedEntity.Description;
            oldEntity.HasVideo = changedEntity.HasVideo;
            oldEntity.Name = changedEntity.Name;
        }
    }
}
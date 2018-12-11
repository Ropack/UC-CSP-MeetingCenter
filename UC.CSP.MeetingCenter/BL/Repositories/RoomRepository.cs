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
            return Context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public Room GetByCode(string code)
        {
            return Context.Rooms.FirstOrDefault(r => r.Code == code);
        }

        public override void Create(Room entity)
        {
            VerifyConstraints(entity);

            Context.NoteChange();
            entity.Id = Context.Rooms.Max(r => r.Id) + 1;
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
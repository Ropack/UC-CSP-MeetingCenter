using System;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public override Reservation GetById(int id)
        {
            var entity = Context.Reservations.FirstOrDefault(r => r.Id == id);
            if (entity != null)
            {
                //entity.MeetingRoom = Context.Rooms.Single(r => r.Id == entity.MeetingRoomId);
            }
            return entity;
        }

        public override void Create(Reservation entity)
        {
            VerifyConstraints(entity);

            Context.NoteChange();
            entity.Id = Context.Reservations.Any() ? Context.Reservations.Max(r => r.Id) + 1 : 1;

            Context.Rooms.Single(c => c.Id == entity.MeetingRoomId).Reservations.Add(entity);
            Context.Reservations.Add(entity);
        }

        public override void Update(Reservation entity)
        {
            VerifyConstraints(entity);
            var oldEntity = GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new ArgumentException("Updated room does not exists.");
            }
            
            Context.NoteChange();
            Map(entity, oldEntity);
        }

        public override void Delete(Reservation entity)
        {
            Context.NoteChange();
            Context.Reservations.Remove(entity);
        }

        private void VerifyConstraints(Reservation entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var reservations = Context.Reservations
                .Where(r => r.MeetingRoomId == entity.MeetingRoomId)
                .Where(r => r.Date.Date == entity.Date.Date)
                .Where(r => r.TimeFrom > entity.TimeFrom && r.TimeFrom < entity.TimeTo ||
                    r.TimeFrom < entity.TimeFrom && r.TimeTo > entity.TimeFrom);
            if (reservations.Any())
            {
                //TODO: Make this as validation error
                throw new Exception("Overlapping reservations.");
            }
        }

        private void Map(Reservation changedEntity, Reservation oldEntity)
        {
            oldEntity.Customer = changedEntity.Customer;
            oldEntity.ExpectedPersonsCount = changedEntity.ExpectedPersonsCount;
            oldEntity.Note = changedEntity.Note;
            oldEntity.TimeFrom = changedEntity.TimeFrom;
            oldEntity.TimeTo = changedEntity.TimeTo;
            oldEntity.VideoConference = changedEntity.VideoConference;
            oldEntity.Date = changedEntity.Date;
            oldEntity.Date = changedEntity.Date;
        }
    }
}
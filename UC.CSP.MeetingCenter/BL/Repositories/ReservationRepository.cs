using System;
using System.Data;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public override Reservation GetById(int id)
        {
            return Context.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public override void Create(Reservation entity)
        {
            VerifyConstraints(entity);

            Context.NoteChange();
            entity.Id = Context.Rooms.Max(r => r.Id) + 1;
            Context.Reservations.Add(entity);
        }

        public override void Update(Reservation entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
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
using System;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public override void Create(Reservation entity)
        {
            VerifyConstraints(entity);
            Context.Reservations.Add(entity);
        }

        private void VerifyConstraints(Reservation entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var reservations = Context.Reservations
                .Where(r => r.RoomId == entity.RoomId)
                .Where(r => r.Date.Date == entity.Date.Date)
                .Where(r => r.TimeFrom > entity.TimeFrom && r.TimeFrom < entity.TimeTo ||
                    r.TimeFrom < entity.TimeFrom && r.TimeTo > entity.TimeFrom);
            if (reservations.Any())
            {
                //TODO: Make this as validation error
                throw new Exception("Overlapping reservations.");
            }
        }
    }
}
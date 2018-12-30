using System.Collections.Generic;
using System.Linq;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Queries
{
    public class ReservationsQuery : QueryBase
    {
        private ReservationFilterDTO Filter { get; }
        public ReservationsQuery(ReservationFilterDTO filter)
        {
            Filter = filter;
        }
        public List<Reservation> Execute()
        {
            var reservations = Context.Reservations
                .Where(r => r.RoomId == Filter.RoomId)
                .Where(r => r.Date.Date == Filter.DateTime.Date)
                .OrderBy(r => r.TimeFrom).ToList();

            return reservations;
        }
        
    }
}
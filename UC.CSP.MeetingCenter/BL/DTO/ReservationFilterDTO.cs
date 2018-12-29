using System;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.DTO
{
    public class ReservationFilterDTO
    {
        public DateTime DateTime { get; set; }
        public int RoomId { get; set; }
    }
}
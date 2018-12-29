using System.Collections.Generic;
using System.Xml.Serialization;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL
{
    public class InMemoryDatabaseContext : IDatabaseContext
    {
        public List<Center> Centers { get; private set; } = new List<Center>();
        public List<Room> Rooms { get; private set; } = new List<Room>();
        public List<Reservation> Reservations { get; private set; } = new List<Reservation>();
        [XmlIgnore]
        public bool Changed { get; set; }
        public void NoteChange()
        {
            Changed = true;

        }
    }
}
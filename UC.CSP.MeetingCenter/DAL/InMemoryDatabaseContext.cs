using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL
{
    public class InMemoryDatabaseContext : IDatabaseContext
    {
        private bool changesTracked;
        public List<Center> Centers { get; private set; } = new List<Center>();
        public List<Room> Rooms { get; private set; } = new List<Room>();
        public List<Reservation> Reservations { get; private set; } = new List<Reservation>();
        [XmlIgnore]
        public bool Changed { get; private set; }
        public void TrackChanges()
        {
            changesTracked = true;
        }
        public void NoteChange()
        {
            if (changesTracked)
            {
                Changed = true;
            }
        }
    }
}
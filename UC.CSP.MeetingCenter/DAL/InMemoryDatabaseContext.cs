using System;
using System.Collections.Generic;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL
{
    public class InMemoryDatabaseContext : IDatabaseContext
    {
        public List<Center> Centers { get; private set; } = new List<Center>();
        public List<Room> Rooms { get; private set; } = new List<Room>();
    }
}
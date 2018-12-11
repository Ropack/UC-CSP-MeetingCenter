using System;
using System.Collections.Generic;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.DAL
{
    public interface IDatabaseContext
    {
        List<Center> Centers { get; }
        List<Room> Rooms { get; }
    }
}
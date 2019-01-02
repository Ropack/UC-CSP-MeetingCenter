using System;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public interface ISoftDeletable
    {
        DateTime? DeletedDate { get; set; }
    }
}
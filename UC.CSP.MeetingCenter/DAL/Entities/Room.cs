using System;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool HasVideo { get; set; }
        public string CenterCode { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
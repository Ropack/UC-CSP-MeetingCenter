using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Center : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [XmlIgnore]
        public List<Room> Rooms { get;  set; } = new List<Room>();
        public override string ToString()
        {
            return Name;
        }
    }
}
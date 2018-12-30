using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Data.Entity;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Center : IEntity
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Code { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Description { get; set; }

        [XmlIgnore]
        public virtual ICollection<Room> Rooms { get; private set; } = new List<Room>();
        public override string ToString()
        {
            return Name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Xml.Serialization;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Room : IEntity
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
        [Range(1,100)]
        public int Capacity { get; set; }
        public bool HasVideo { get; set; }
        [XmlIgnore]
        [ForeignKey(nameof(CenterId))]
        public virtual Center Center { get; set; }

        public int CenterId { get; set; }

        [XmlIgnore]
        public virtual ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
        public override string ToString()
        {
            return Name;
        }
    }
}
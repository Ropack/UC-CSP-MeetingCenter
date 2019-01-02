using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class StockOperation : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
        [ForeignKey(nameof(AccessoryId))]
        public Accessory Accessory { get; set; }
        public int AccessoryId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CustomerName { get; set; }
    }
}
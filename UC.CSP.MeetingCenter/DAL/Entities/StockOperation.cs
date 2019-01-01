using System;
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
    }
}
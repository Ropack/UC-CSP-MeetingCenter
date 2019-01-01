using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Accessory : IEntity
    {
        public int Id { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Range(0,1000)]
        public int StoredCount { get; set; }
        [Range(0, 1000)]
        public int RecommendedMinCount { get; set; }

        public DateTime? DeletedDate { get; set; }

    }
}
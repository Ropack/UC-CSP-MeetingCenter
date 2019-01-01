using System.ComponentModel.DataAnnotations;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }
}
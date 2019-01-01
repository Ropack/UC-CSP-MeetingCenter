using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Queries
{
    public class AccessoriesQuery : QueryBase
    {
        public int CategoryId { get; set; }
        public List<AccessoryDTO> Execute()
        {
            IQueryable<Accessory> q = Context.Accessories.OrderBy(a => a.Category.Name).ThenBy(a => a.Name);
            if (CategoryId != 0)
            {
                q = q.Where(a => a.CategoryId == CategoryId);
            }
            return q.ProjectTo<AccessoryDTO>().ToList();
        }
    }
}
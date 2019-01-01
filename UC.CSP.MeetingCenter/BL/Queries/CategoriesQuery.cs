using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using AutoMapper.QueryableExtensions;
using UC.CSP.MeetingCenter.BL.DTO;

namespace UC.CSP.MeetingCenter.BL.Queries
{
    public class CategoriesQuery : QueryBase
    {
        public List<CategoryDTO> Execute()
        {
            return Context.Categories.ProjectTo<CategoryDTO>().ToList();
        }
    }
}
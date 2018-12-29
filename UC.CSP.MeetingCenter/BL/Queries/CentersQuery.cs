using System.Collections.Generic;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Queries
{
    public class CentersQuery : QueryBase
    {
        public List<Center> Execute()
        {
            var centers = Context.Centers.ToList();

            return centers;
        }
    }
}
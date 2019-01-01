using System;
using System.Data;
using System.Linq;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class CenterRepository : RepositoryBase<Center>
    {
        public Center GetByCode(string code)
        {
            return Context.Centers.FirstOrDefault(r => r.Code == code);
        }
    }
}
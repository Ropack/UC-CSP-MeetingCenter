using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Queries
{
    public abstract class QueryBase
    {
        protected IDatabaseContext Context => DatabaseContextFactory.GetContext();
    }
}
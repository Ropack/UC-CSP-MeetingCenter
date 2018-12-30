using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Facades
{
    public class FacadeBase
    {
        protected AppUnitOfWorkProvider UnitOfWorkProvider => AppUnitOfWorkProvider.Instance;
    }
}
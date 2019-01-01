using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Repositories
{
    public class StockOperationRepository : RepositoryBase<StockOperation>
    {
        public override void Create(StockOperation entity)
        {
            Context.StockOperations.Add(entity);
        }

        public override void Delete(StockOperation entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
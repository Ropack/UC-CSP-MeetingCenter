using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace UC.CSP.MeetingCenter.DAL
{
    public class AppDbConfiguration : DbConfiguration
    {
        public AppDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
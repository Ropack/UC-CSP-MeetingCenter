using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace UC.CSP.MeetingCenter.DAL
{
    public class DatabaseContextFactory
    {
        public static AppDbContext GetContext()
        {
            // This is single point in app to determine which DB Context use
            return new AppDbContext();
        }
    }
}
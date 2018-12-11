using System;

namespace UC.CSP.MeetingCenter.DAL
{
    public class DatabaseContextFactory
    {
        private static IDatabaseContext instance = new InMemoryDatabaseContext();

        public static IDatabaseContext GetContext()
        {
            // This is single point in app to determine which DB Context use
            return instance;
        }

        public static void SetContext(IDatabaseContext context)
        {
            instance = context;
        }
    }
}
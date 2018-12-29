using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;

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
            foreach (var center in instance.Centers)
            {
                center.Rooms = instance.Rooms.Where(r => r.CenterCode == center.Code).ToList();
                // this is definitely bad, but we don't use database and have only few data
                // entity framework would take care of that
                foreach (var room in center.Rooms)
                {
                    room.Reservations = instance.Reservations.Where(r => r.MeetingRoomId == room.Id).ToList();
                    foreach (var reservation in room.Reservations)
                    {
                        reservation.MeetingRoom = room;
                    }
                }
            }
        }
    }
}
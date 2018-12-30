using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using UC.CSP.MeetingCenter.DAL;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.Services
{
    public class CsvImportService
    {
        public void Import(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                var context = DatabaseContextFactory.GetContext();
                ClearContext(context);
                ParseCsv(sr, context);
            }
        }

        private void ParseCsv(StreamReader sr, AppDbContext context)
        {
            sr.ReadLine(); //skip first line
            var centers = ParseCenters(sr);
            var rooms = ParseRooms(sr);
            PairRoomsToCenters(centers, rooms);
            AddDataToContext(context, centers, rooms);
        }

        private void AddDataToContext(AppDbContext context, List<Center> centers, List<Room> rooms)
        {
            foreach (var center in centers)
            {
                context.Centers.Add(center);
            }
            foreach (var room in rooms)
            {
                context.Rooms.Add(room);
            }
        }

        private void PairRoomsToCenters(List<Center> centers, List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                throw new NotImplementedException();
                //centers.First(c => c.Code == room.CenterCode).Rooms.Add(room);
            }
        }

        private List<Center> ParseCenters(StreamReader sr)
        {
            var centers = new List<Center>();
            string s;
            var i = 1;
            while (!(s = sr.ReadLine()).StartsWith("MEETING_ROOMS"))
            {
                var values = s.Split(',');
                centers.Add(new Center()
                {
                    Id = i++,
                    Name = values[0],
                    Code = values[1],
                    Description = values[2]
                });
            } 

            return centers;
        }

        private List<Room> ParseRooms(StreamReader sr)
        {
            var rooms = new List<Room>();
            string s;
            var i = 1;
            while ((s = sr.ReadLine()) != null)
            {
                var values = s.Split(',');
                rooms.Add(new Room()
                {
                    Id = i++,
                    Name = values[0],
                    Code = values[1],
                    Description = values[2],
                    Capacity = Convert.ToInt32(values[3]),
                    HasVideo = values[4] == "YES"//,
                    //CenterCode = values[5] //TODO: code
                });
            }

            return rooms;
        }

        private void ClearContext(AppDbContext context)
        {
            var rooms = context.Set<Room>();
            context.Rooms.RemoveRange(rooms);
            var centers = context.Set<Center>();
            context.Centers.RemoveRange(centers);
            //context.SaveChanges();
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.DAL;

namespace UC.CSP.MeetingCenter.BL.Services
{
    public class JsonExportService
    {

        public void Export(string fileName)
        {
            var context =  DatabaseContextFactory.GetContext();


            var json = JsonConvert.SerializeObject(ConvertToExportJsonDTO(context), Formatting.Indented);

            using (var sw = new StreamWriter(fileName))
            {
                sw.Write(json);
            }
        }

        public ExportJsonDTO ConvertToExportJsonDTO(AppDbContext context)
        {
            var dto = new ExportJsonDTO()
            {
                data = new List<Data>()
            };
            var rooms = context.Rooms.Where(r => r.Reservations.Any());
            foreach (var room in rooms)
            {
                var data = new Data()
                {
                    meetingCentre = room.Center.Code,
                    meetingRoom = room.Code,
                    reservations = new Dictionary<string, List<JsonReservationDTO>>()
                };
                foreach (var roomReservation in room.Reservations.OrderBy(r => r.Date.Date))
                {
                    var date = roomReservation.Date.Date.ToString("dd.MM.yyyy");
                    if (data.reservations.ContainsKey(date))
                    {
                    }
                    else
                    {
                        data.reservations.Add(date, new List<JsonReservationDTO>());
                    }
                    data.reservations[date].Add(new JsonReservationDTO()
                    {
                        expectedPersonsCount = roomReservation.ExpectedPersonsCount,
                        customer = roomReservation.Customer,
                        from = $"{roomReservation.TimeFrom:hh\\:mm}",
                        to = $"{roomReservation.TimeTo:hh\\:mm}",
                        note = roomReservation.Note,
                        videoConference = roomReservation.VideoConference
                    });
                }
                dto.data.Add(data);

            }


            return dto;
        }
    }
}
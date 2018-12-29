using System;
using System.Collections.Generic;

namespace UC.CSP.MeetingCenter.BL.DTO
{
    public class ExportJsonDTO
    {
        public string schema { get; set; } = "PLUS4U.EBC.MCS.MeetingRoom_Schedule_1.0";
        public string uri { get; set; } = "ues:UCL-BT:UCL.INF/DEMO_REZERVACE:EBC.MCS.DEMO/MR001/SCHEDULE";
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string meetingCentre { get; set; }
        public string meetingRoom { get; set; }
        public Dictionary<string, List<JsonReservationDTO>> reservations { get; set; }
    }

    public class JsonReservationDTO
    {
        public string from { get; set; }
        public string to { get; set; }
        public int expectedPersonsCount { get; set; }
        public string customer { get; set; }
        public bool videoConference { get; set; }
        public string note { get; set; }
    }
}
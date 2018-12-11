using System;
using UC.CSP.MeetingCenter.BL;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Reservation : IEntity
    {
        private int expectedPersonsCount;
        private string customer;
        private bool videoConference;
        private string note;
        public int Id { get; set; }
        public Room MeetingRoom { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public int ExpectedPersonsCount
        {
            get => expectedPersonsCount;
            set
            {
                if (value < 1)
                {
                    throw new ValidationException("Expected persons count cannot be lower than 1.");
                }
                if (value > MeetingRoom.Capacity)
                {
                    throw new ValidationException("Expected persons count cannot be higher than the capacity of the room.");
                }

                expectedPersonsCount = value;
            }
        }

        public string Customer
        {
            get => customer;
            set
            {
                if (value.Length < 2 || value.Length > 100)
                {
                    throw new ValidationException("Customer name length must be between 2 and 100 characters.");
                }
                customer = value;
            }
        }

        public bool VideoConference
        {
            get => videoConference;
            set
            {
                if (value && !MeetingRoom.HasVideo)
                {
                    throw new ValidationException("Selected room does not support video conference.");
                }
                videoConference = value;
            }
        }

        public string Note
        {
            get => note;
            set
            {
                if (value.Length > 300)
                {
                    throw new ValidationException("Note cannot be longer than 300 characters.");
                }
                note = value;
            }
        }
    }
}
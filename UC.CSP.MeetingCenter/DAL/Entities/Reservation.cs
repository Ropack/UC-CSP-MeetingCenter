﻿using System;
using System.Collections.Generic;
using System.Linq;
using UC.CSP.MeetingCenter.BL;
using UC.CSP.MeetingCenter.BL.Validation;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public class Reservation : IEntity, IValidatable
    {
        public int Id { get; set; }
        public virtual Room MeetingRoom { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int ExpectedPersonsCount { get; set; }
        public string Customer { get; set; }
        public bool VideoConference { get; set; }
        public string Note { get; set; }

        public void Validate()
        {
            var validationErrors = new List<ValidationError>();
            if (ExpectedPersonsCount < 1)
            {
                validationErrors.Add(new ValidationError("Expected persons count cannot be lower than 1."));
            }
            if (ExpectedPersonsCount > MeetingRoom.Capacity)
            {
                validationErrors.Add(new ValidationError("Expected persons count cannot be higher than the capacity of the room."));
            }
            if (Customer.Length < 2 || Customer.Length > 100)
            {
                validationErrors.Add(new ValidationError("Customer name length must be between 2 and 100 characters."));
            }
            if (VideoConference && !MeetingRoom.HasVideo)
            {
                validationErrors.Add(new ValidationError("Selected room does not support video conference."));
            }
            if (Note.Length > 300)
            {
                validationErrors.Add(new ValidationError("Note cannot be longer than 300 characters."));
            }

            if (validationErrors.Any())
            {
                throw new ValidationException(validationErrors);
            }
        }
    }
}
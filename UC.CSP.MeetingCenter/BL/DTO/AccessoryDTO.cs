using System;
using System.Collections.Generic;
using System.Linq;
using UC.CSP.MeetingCenter.BL.Validation;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.DTO
{
    public class AccessoryDTO : IValidatable
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int StoredCount { get; set; }
        public int RecommendedMinCount { get; set; }
        public void Validate()
        {
            var validationErrors = new List<ValidationError>();
            Validate(validationErrors);
        }

        public void Validate(List<ValidationError> validationErrors)
        {
            if (RecommendedMinCount < 0)
            {
                validationErrors.Add(new ValidationError("Recommended minimum count cannot be negative number."));
            }
            if (RecommendedMinCount > 1000)
            {
                validationErrors.Add(new ValidationError("Recommended minimum count must be number between 0 and 1000."));
            }
            if (Name.Length < 2 || Name.Length > 100)
            {
                validationErrors.Add(new ValidationError("Accessory name length must be between 2 and 100 characters."));
            }
            if (CategoryId <= 0)
            {
                validationErrors.Add(new ValidationError("Please select category."));
            }
            if (validationErrors.Any())
            {
                throw new ValidationException(validationErrors);
            }
        }
    }
}
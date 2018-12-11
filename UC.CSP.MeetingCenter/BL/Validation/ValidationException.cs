using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UC.CSP.MeetingCenter.BL.Validation
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> ValidationErrors { get; set; } 
        public ValidationException(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
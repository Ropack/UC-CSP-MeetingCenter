﻿using System;
using System.Collections.Generic;

namespace UC.CSP.MeetingCenter.BL.Validation
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public ValidationException(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException(ValidationError validationError)
        {
            ValidationErrors = new List<ValidationError>() { validationError };
        }
    }
}
using System.Collections.Generic;
using UC.CSP.MeetingCenter.BL.Validation;

namespace UC.CSP.MeetingCenter.DAL.Entities
{
    public interface IValidatable
    {
        void Validate();
        void Validate(List<ValidationError> validationErrors);
    }
}
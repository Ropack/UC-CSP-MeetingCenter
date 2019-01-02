using System.Collections.Generic;
using UC.CSP.MeetingCenter.APP;
using UC.CSP.MeetingCenter.BL.Validation;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL.DTO
{
    public class AccessoryStockDTO : IValidatable
    {
        public AccessoryDTO Accessory { get; set; }
        public StockFormMode Mode { get; set; }
        public int Count { get; set; }
        public string CustomerName { get; set; }
        public void Validate()
        {
            Validate(new List<ValidationError>());
        }

        public void Validate(List<ValidationError> validationErrors)
        {
            if (Count < 1)
            {
                validationErrors.Add(new ValidationError("Count must be a positive number."));
            }
            if (Mode == StockFormMode.In)
            {
                if (Accessory.StoredCount + Count > 1000)
                {
                    validationErrors.Add(new ValidationError(
                        $"There is not enough space in the stock. You can add only {1000 - Accessory.StoredCount} of {Accessory.Name}."));
                }
            }
            else
            {
                if (Accessory.StoredCount - Count < 0)
                {
                    validationErrors.Add(new ValidationError(
                        $"There is not enough {Accessory.Name} in the stock. There is only {Accessory.StoredCount} of {Accessory.Name} in stock right now."));
                }

                if (CustomerName.Length < 2 || CustomerName.Length > 100)
                {
                    validationErrors.Add(new ValidationError("Customer name length must be between 2 and 100 characters."));
                }
            }
            Accessory.Validate(validationErrors);
        }
    }
}
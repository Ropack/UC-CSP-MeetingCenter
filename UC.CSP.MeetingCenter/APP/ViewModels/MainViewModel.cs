using System.Collections.Generic;
using UC.CSP.MeetingCenter.BL.DTO;

namespace UC.CSP.MeetingCenter.APP.ViewModels
{
    public class MainViewModel
    {
        public List<AccessoryDTO> Accessories { get; set; } = new List<AccessoryDTO>()
        {
            new AccessoryDTO()
            {
                Name = "tt",
                Category = "ooo",
                RecommendedMinCount = 10,
                StoredCount = 5
            }
        };
        public string Test { get; set; } = "Test in MVM";
    }
}
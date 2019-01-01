using AutoMapper;
using UC.CSP.MeetingCenter.BL.DTO;
using UC.CSP.MeetingCenter.DAL.Entities;

namespace UC.CSP.MeetingCenter.BL
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Accessory, AccessoryDTO>()
                    .ForMember(d => d.Category, m => m.MapFrom(o => o.Category.Name));
                mapper.CreateMap<AccessoryDTO, Accessory>()
                    .ForMember(d => d.Category, m => m.Ignore())
                    .ForMember(d => d.DeletedDate, m => m.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
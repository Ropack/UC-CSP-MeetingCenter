using System;
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
                mapper.CreateMap<AccessoryStockDTO, StockOperation>()
                    .ForMember(d => d.Accessory, m => m.Ignore())
                    .ForMember(d => d.AccessoryId, m => m.MapFrom(o => o.Accessory.Id))
                    .ForMember(d => d.DateTime, m => m.MapFrom(o => DateTime.UtcNow))
                    .ForMember(d => d.Id, m => m.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
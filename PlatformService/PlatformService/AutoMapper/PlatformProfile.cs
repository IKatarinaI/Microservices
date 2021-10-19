using AutoMapper;
using PlatformService.DTOs.CreateDTOs;
using PlatformService.DTOs.ReadDTOs;
using PlatformService.Models;

namespace PlatformService.AutoMapper
{
    public class PlatformProfile: Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, ReadPlatformDTO>();
            CreateMap<CreatePlatformDTO, Platform>();
        }
    }
}

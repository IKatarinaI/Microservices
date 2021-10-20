using AutoMapper;
using CommandsService.DTOs.ReadDTOs;
using CommandsService.Models;

namespace CommandsService.AutoMapper
{
    public class PlatformProfile: Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, ReadPlatformDTO>();
        }
    }
}

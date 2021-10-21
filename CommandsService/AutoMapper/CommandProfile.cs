using AutoMapper;
using CommandsService.DTOs;
using CommandsService.DTOs.CreateDTOs;
using CommandsService.DTOs.ReadDTOs;
using CommandsService.Models;

namespace CommandsService.AutoMapper
{
    public class CommandProfile: Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, ReadCommandDTO>();
            CreateMap<CreateCommandDTO, Command>();
            CreateMap<PlatformPublishedDTO, Platform>()
                     .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

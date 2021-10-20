using AutoMapper;
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
        }
    }
}

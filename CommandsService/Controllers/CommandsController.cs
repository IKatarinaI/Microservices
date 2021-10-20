using AutoMapper;
using CommandsService.DTOs.CreateDTOs;
using CommandsService.DTOs.ReadDTOs;
using CommandsService.Models;
using CommandsService.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandServiceRepository _commandServiceRepository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandServiceRepository commandServiceRepository, IMapper mapper)
        {
            _commandServiceRepository = commandServiceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCommandDTO>> GetCommandsForPlatform(int platformId)
        {
            if (!_commandServiceRepository.PlatformExists(platformId))
                return NotFound();

            var commands = _commandServiceRepository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<ReadCommandDTO>>(commands));
        }

        [HttpGet("{commandId}", Name ="GetCommandForPlatform")]
        public ActionResult<ReadCommandDTO> GetCommandForPlatform(int platformId, int commandIt)
        {
            if (!_commandServiceRepository.PlatformExists(platformId))
                return NotFound();

            var command = _commandServiceRepository.GetCommand(platformId, commandIt);

            if (command is null)
                return NotFound();

            return Ok(_mapper.Map<ReadCommandDTO>(command));
        }

        [HttpPost]
        public ActionResult<ReadCommandDTO> CreateCommandForPlatform(int platformId, CreateCommandDTO createCommandDTO)
        {
            if (!_commandServiceRepository.PlatformExists(platformId))
                return NotFound();

            var command = _mapper.Map<Command>(createCommandDTO);

            _commandServiceRepository.CreateCommand(platformId, command);
            _commandServiceRepository.SaveChanges();

            var readCommandDTO = _mapper.Map<ReadCommandDTO>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                   new { platformId = platformId, commandId = readCommandDTO.Id }, readCommandDTO);
        }
    }
}

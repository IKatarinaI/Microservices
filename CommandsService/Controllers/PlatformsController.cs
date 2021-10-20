using AutoMapper;
using CommandsService.DTOs.ReadDTOs;
using CommandsService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandServiceRepository _commandServiceRepository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandServiceRepository commandServiceRepository, IMapper mapper)
        {
            _commandServiceRepository = commandServiceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadPlatformDTO>> GetPlatforms()
        {
            var platforms = _commandServiceRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<ReadPlatformDTO>>(platforms));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        { 
            return Ok("Inbound test from CommandsService.");
        }
    }
}

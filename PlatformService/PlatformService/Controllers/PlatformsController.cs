using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices.Interfaces;
using PlatformService.DTOs;
using PlatformService.DTOs.CreateDTOs;
using PlatformService.DTOs.ReadDTOs;
using PlatformService.Models;
using PlatformService.Repositories.Interfaces;
using PlatformService.SyncDataServices.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformServiceRepository _platformServiceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpCommandDataClient _httpCommandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformServiceRepository platformServiceRepository,
                                   IMapper mapper,
                                   IHttpCommandDataClient httpCommandDataClient,
                                   IMessageBusClient messageBusClient)
        {
            _platformServiceRepository = platformServiceRepository;
            _mapper = mapper;
            _httpCommandDataClient = httpCommandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadPlatformDTO>> GetPlatforms()
        {
            var platforms = _platformServiceRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<ReadPlatformDTO>>(platforms));
        }

        [HttpGet("/{id}")]
        public ActionResult<ReadPlatformDTO> GetPlatformById(int id)
        {
            var platform = _platformServiceRepository.GetPlatformbyID(id);

            if (platform is null)
                return NotFound();

            return Ok(_mapper.Map<ReadPlatformDTO>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<ReadPlatformDTO>> CreatePlatform(CreatePlatformDTO createPlatformDTO)
        {
            var platform = _mapper.Map<Platform>(createPlatformDTO);
            _platformServiceRepository.CreatePlatform(platform);
            _platformServiceRepository.SaveChanges();
            var createdPlatform = _mapper.Map<ReadPlatformDTO>(platform);

            // send sync message
            try
            {
                await _httpCommandDataClient.SendPlatformToCommand(createdPlatform);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not send synchronously: {ex.Message}");
            }

            // send async message
            try
            {
                var platformPublishDTO = _mapper.Map<PlatformPublishedDTO>(createdPlatform);
                platformPublishDTO.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not send asynchronously: {ex.Message}");
            }

            return Ok(createdPlatform);
        }
    }
}

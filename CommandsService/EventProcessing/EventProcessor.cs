using AutoMapper;
using CommandsService.DTOs;
using CommandsService.EventProcessing.Interfaces;
using CommandsService.Models;
using CommandsService.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using static CommandsService.Enums.Enums;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("Determining event");
            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);
            
            switch(eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform published event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("Could not determine event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandServiceRepository>();
                var platformPublishedDTO = JsonSerializer.Deserialize<PlatformPublishedDTO>(platformPublishedMessage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDTO);

                    if(!repo.ExternalPlatformExists(platform.ExternalId))
                    {
                        repo.CreatePlatform(platform);
                        repo.SaveChanges();
                        Console.WriteLine("Platform added.");
                    }
                    else
                    {
                        Console.WriteLine("Platform already exists");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Could not add Platform to DB: {ex.Message}");
                }
            }
        }
    }
}

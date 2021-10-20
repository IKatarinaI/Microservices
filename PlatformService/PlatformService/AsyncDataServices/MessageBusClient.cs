using Microsoft.Extensions.Configuration;
using PlatformService.AsyncDataServices.Interfaces;
using PlatformService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO)
        {
            throw new NotImplementedException();
        }
    }
}

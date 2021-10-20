using PlatformService.DTOs;

namespace PlatformService.AsyncDataServices.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO);
    }
}

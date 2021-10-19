using PlatformService.DTOs.ReadDTOs;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http.Interfaces
{
    public interface IHttpCommandDataClient
    {
        Task SendPlatformToCommand(ReadPlatformDTO readPlatformDTO);
    }
}

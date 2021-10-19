using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Repositories.Interfaces
{
    public interface IPlatformServiceRepository
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformbyID(int id);
        void CreatePlatform(Platform platform);
    }
}

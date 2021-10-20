using CommandsService.Models;
using System.Collections.Generic;

namespace CommandsService.Repositories.Interfaces
{
    public interface ICommandServiceRepository
    {
        bool SaveChanges();

        // platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);

        // commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        void CreateCommand(int platformId, Command command);
        Command GetCommand(int platformId, int commandId);
    }
}

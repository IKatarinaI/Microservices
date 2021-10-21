using CommandsService.DBAccess;
using CommandsService.Models;
using CommandsService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Repositories
{
    public class CommandServiceRepository : ICommandServiceRepository
    {
        private readonly CommandServiceDbContext _commandServiceDbContext;

        public CommandServiceRepository(CommandServiceDbContext commandServiceDbContext)
        {
            _commandServiceDbContext = commandServiceDbContext;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            command.PlatformId = platformId;

            _commandServiceDbContext.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
                throw new ArgumentNullException(nameof(platform));

            _commandServiceDbContext.Platforms.Add(platform);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _commandServiceDbContext.Commands
                   .Where(c => c.PlatformId == platformId)
                   .OrderBy(c => c.Platform.Name);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _commandServiceDbContext.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _commandServiceDbContext.Commands
                   .Where(c => c.PlatformId == platformId && c.Id == commandId)
                   .FirstOrDefault();
        }

        public bool PlatformExists(int platformId)
        {
            return _commandServiceDbContext.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_commandServiceDbContext.SaveChanges() >= 0);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _commandServiceDbContext.Platforms.Any(p => p.ExternalId == externalPlatformId);
        }
    }
}

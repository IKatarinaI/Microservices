using PlatformService.DBAccess;
using PlatformService.Models;
using PlatformService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformService.Repositories
{
    public class PlatformServiceRepository : IPlatformServiceRepository
    {
        private PlatformServiceDbContext _dbContext;

        public PlatformServiceRepository(PlatformServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _dbContext.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _dbContext.Platforms.ToList();
        }

        public Platform GetPlatformbyID(int id)
        {
            return _dbContext.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}

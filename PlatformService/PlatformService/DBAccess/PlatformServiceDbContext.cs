using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.DBAccess
{
    public class PlatformServiceDbContext: DbContext
    {
        public PlatformServiceDbContext(DbContextOptions<PlatformServiceDbContext> opt) : base(opt)
        { 
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}

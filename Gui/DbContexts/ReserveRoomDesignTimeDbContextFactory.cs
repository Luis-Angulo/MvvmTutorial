using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gui.DbContexts
{
    // EFCore picks up DesignTimeContextFactory instances by convention when generating a migration from nuget PM console
    // Once the migration file is generated this file isn't called anymore so it can be left out from version control
    // I chose to retain it here as an example implementation
    public class ReserveRoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReserveRoomDbContext>
    {
        public ReserveRoomDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=reserveroom.db")
                .Options;
            return new ReserveRoomDbContext(options);
        }
    }
}

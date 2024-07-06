using Microsoft.EntityFrameworkCore;

namespace Gui.DbContexts
{
    public class ReserveRoomDbContextFactory
    {
        private readonly string _ConnectionString;

        public ReserveRoomDbContextFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public ReserveRoomDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(_ConnectionString)
                .Options;
            return new ReserveRoomDbContext(options);
        }
    }
}

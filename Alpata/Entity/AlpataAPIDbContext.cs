using Microsoft.EntityFrameworkCore;

namespace Alpata.Entity
{
	public class AlpataAPIDbContext : DbContext
	{
		public AlpataAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}

using JwtWebAPI.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace JwtWebAPI.Model
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}

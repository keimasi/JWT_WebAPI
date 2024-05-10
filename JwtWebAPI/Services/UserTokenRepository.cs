using JwtWebAPI.Model;
using JwtWebAPI.Model.Entity;

namespace JwtWebAPI.Services
{
    public class UserTokenRepository
    {
        private readonly DataBaseContext _context;

        public UserTokenRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void SaveToken(UserToken command)
        {
            _context.UserTokens.Add(command);
            _context.SaveChanges();
        }
    }
}

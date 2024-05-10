using JwtWebAPI.Model;
using JwtWebAPI.Model.Entity;

namespace JwtWebAPI.Services
{
    public class UserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public User? GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            return user;
        }

        //public bool ValidateUser(string userName, string password)
        //{
        //    var user=_context.Users.FirstOrDefault(x=>x.)
        //}
    }
}

namespace JwtWebAPI.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<UserToken> Tokens { get; set; }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtWebAPI.Services.Validator;

public class TokenValidator : ITokenValidator
{
    private readonly UserRepository _userRepository;

    public TokenValidator(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(TokenValidatedContext context)
    {
        var claims = context.Principal.Identity as ClaimsIdentity;

        if (claims.Claims == null)
        {
            context.Fail("Claims Not Found !");
            return;
        }

        var userId = int.Parse(claims.FindFirst("UserId").Value);

        var user = _userRepository.GetUser(userId);

        if (!user.IsActive)
        {
            context.Fail("User not Active");
            return;
        }
    }
}
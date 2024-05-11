using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtWebAPI.Services.Validator
{
    public interface ITokenValidator
    {
        Task Execute(TokenValidatedContext context);
    }
}

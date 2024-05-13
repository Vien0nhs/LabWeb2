using Microsoft.AspNetCore.Identity;

namespace Lab_Web2.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
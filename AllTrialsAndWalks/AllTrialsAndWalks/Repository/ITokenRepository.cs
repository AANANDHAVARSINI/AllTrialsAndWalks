using Microsoft.AspNetCore.Identity;

namespace AllTrialsAndWalks.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

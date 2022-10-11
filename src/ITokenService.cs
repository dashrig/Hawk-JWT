using HawkMiddlewares.Data;

namespace HawkMiddlewares
{

    public interface ITokenService
    {
        string BuildToken(string key, string issuer, XAuthData user);
        bool ValidateToken(string key, string issuer, string audience, string token, out XAuthData authData);
    }
}

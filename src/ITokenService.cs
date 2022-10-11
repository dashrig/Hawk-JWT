using HawkMiddlewares.Data;

namespace HawkMiddlewares
{

    public interface ITokenService<TUserIdType> where TUserIdType : IConvertible
    {
        string BuildToken(string key, string issuer, XAuthData<TUserIdType> user);
        bool ValidateToken(string key, string issuer, string audience, string token, out XAuthData<TUserIdType> authData);
    }
}

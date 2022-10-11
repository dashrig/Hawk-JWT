using HawkMiddlewares.Data;

namespace HawkMiddlewares
{
    public abstract class HawkJwtAuthProviderContext : IHawkJwtAuthProvider
        
    {
        public virtual XAuthData GetUserData(XAuthData authData)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsUserExist(XAuthData authData)
        {
            throw new NotImplementedException();
        }
    }
}



using HawkMiddlewares.Data;

namespace HawkMiddlewares
{
    public abstract class HawkJwtAuthProviderContext<TUserIdType> : IHawkJwtAuthProvider<TUserIdType> 
        where TUserIdType : IConvertible
    {
        public virtual XAuthData<TUserIdType> GetUserData(XAuthData<TUserIdType> authData)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsUserExist(XAuthData<TUserIdType> authData)
        {
            throw new NotImplementedException();
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{
    public interface IHawkJwtAuthProvider<TUserIdType> where TUserIdType : IConvertible
    {
         bool IsUserExist(XAuthData<TUserIdType> authData);
         XAuthData<TUserIdType> GetUserData(XAuthData<TUserIdType> authData);
    }
}

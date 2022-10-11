using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{
    public interface IHawkJwtAuthProvider 
    {
         bool IsUserExist(XAuthData authData);
         XAuthData GetUserData(XAuthData authData);
    }
}

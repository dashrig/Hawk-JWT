using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HawkMiddlewares.Data
{
    public class XAuthData<TUserId> where TUserId : IConvertible
    {
        public string Username { get; set; }

        public string Password { get; set; }
        
        public TUserId UserId { get; set; }
        public string XApp { get; set; }
        public string XVersion { get; set; }
        public string XAuth { get; set; }
        public string XSession { get; set; }
        public string XRoles { get; set; }

        public TUserId FromString(string userId)
        {
            return (TUserId) Convert.ChangeType(userId, typeof(TUserId));
        }

        public string ToStringUserId()
        {
            return UserId.ToString();
        }
    }   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HawkMiddlewares.Data
{
    public class XAuthData 
    {
        public string Username { get; set; }

        public string Password { get; set; }
        
        public string UserId { get; set; }
        public string XApp { get; set; }
        public string XVersion { get; set; }
        public string XAuth { get; set; }
        public string XSession { get; set; }
        public string XRoles { get; set; }

        
    }   
}

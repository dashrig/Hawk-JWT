using Microsoft.IdentityModel.Tokens;

namespace HawkMiddlewares
{
    public class JwtOptions
    {
        public JwtOptions()
        {
            Cryptography = SecurityAlgorithms.HmacSha512Signature;
            XAuthHttpContextName = "X-AUTH";
            XAuthHttpHeader = "Authorization";

        }

        public string TokenKey { get;  set; }
        public string TokenIssuer { get;  set; }
        public string TokenAudience { get;  set; }
        public double TokenDefaultExpire { get; set; }
        public double CheckUserInterval { get; set; }
        public bool CheckUserExist { get; set; }
        public string XAuthHttpContextName { get; set; }
        public string XAuthHttpHeader { get; set; }
        public string Cryptography { get; set; }
    }
}



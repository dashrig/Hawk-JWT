using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _config;

        public TokenService(IOptions<JwtOptions> config)
        {
            _config = config.Value;

            if(_config==null)
                throw new ArgumentNullException("No Hawk JWT Option supplied, please declare an option for your token & cryptogphy when register Hawk JWT Service `service.AddHawkJwt<TContext>()`!");
        }

        public string BuildToken(string key, string issuer, XAuthData user) 
        {
            var claims = new[] {
            new Claim("Username", user.Username),
            new Claim("UserId", user.UserId),
            new Claim("XApp", user.XApp.ToString()),
            new Claim("XVersion", user.XVersion),
            new Claim("XSession", Guid.NewGuid().ToString()),
            new Claim("XRoles",user.XRoles)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, _config.Cryptography);
            var tokenDescriptor = new JwtSecurityToken(
                issuer, issuer, claims,
                expires: DateTime.UtcNow.AddDays(_config.TokenDefaultExpire),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public bool IsTokenValid(string key, string issuer, string token, out XAuthData userData)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);

                userData = UnpackTokenData(validatedToken);
            }
            catch
            {
                userData = default;
                return false;
            }

            return true;
        }

        private XAuthData UnpackTokenData(SecurityToken validatedToken) 
        {
            XAuthData userData;
            var jwtToken = (JwtSecurityToken)validatedToken;

            var username = jwtToken.Claims.First(x => x.Type == "Username").Value;
            var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
            var xroles = jwtToken.Claims.First(x => x.Type == "XRoles").Value;
            var xsession = jwtToken.Claims.First(x => x.Type == "XSession").Value;
            var xversion = jwtToken.Claims.First(x => x.Type == "XVersion").Value;
            var xapp = jwtToken.Claims.First(x => x.Type == "XApp").Value;

            userData = new XAuthData()
            {
                Username = username,
                UserId = userId,
                XApp = xapp,
                XSession=xsession,
                XVersion = xversion,
                XRoles = xroles,
            };


            return userData;
        }


        public bool ValidateToken(string key, string issuer, string audience, 
            string token, out XAuthData userData)
        {
            return IsTokenValid(key, issuer, token, out userData);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{

    public class HawkJwtMiddleware 
    {
        private readonly RequestDelegate _next;
        private  ITokenService _token;
        private  JwtOptions _config;

        public HawkJwtMiddleware(RequestDelegate next)
        {
            
            _next = next;
            
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService, IOptions<JwtOptions> config, IHawkJwtAuthProvider provider)
        {
            _token = tokenService;
            _config = config.Value;

            var token = context.Request.Headers[_config.XAuthHttpHeader].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, provider, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IHawkJwtAuthProvider provider, string token)
        {
            try
            {
                XAuthData tokenAuthData;
                var res = _token.ValidateToken(_config.TokenKey, _config.TokenIssuer, _config.TokenAudience, token, out tokenAuthData);

                bool? isUserExist = false;

                if (_config.CheckUserExist)
                    isUserExist = provider?.IsUserExist(tokenAuthData);

                context.Items.Add(_config.XAuthHttpContextName, tokenAuthData);

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}



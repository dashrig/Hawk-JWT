using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{

    public class HawkJwtMiddleware<TUserIdType> where TUserIdType : IConvertible
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService<TUserIdType> _token;
        private readonly JwtOptions _config;

        public HawkJwtMiddleware(RequestDelegate next, ITokenService<TUserIdType> tokenService, IOptions<JwtOptions> config)
        {
            
            _next = next;
            _token = tokenService;
            _config = config.Value;
        }

        public async Task Invoke(HttpContext context, IHawkJwtAuthProvider<TUserIdType> provider)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, provider, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IHawkJwtAuthProvider<TUserIdType> provider, string token)
        {
            try
            {
                XAuthData<TUserIdType> tokenAuthData;
                var res = _token.ValidateToken(_config.TokenKey, _config.TokenIssuer, _config.TokenAudience, token, out tokenAuthData);

                bool? isUserExist = false;

                if (_config.CheckUserExist)
                    isUserExist = provider?.IsUserExist(tokenAuthData);

                context.Items.Add("AUTH", tokenAuthData);

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}



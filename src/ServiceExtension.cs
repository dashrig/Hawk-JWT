using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HawkMiddlewares
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Add all services for using TXWV Hawk JWT Middleware.
        /// </summary>
        /// <typeparam name="TContext">Implementation class of HawkJwtAuthProviderContext</typeparam>
        // <typeparam name="TUserIdType">UserId datatype</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHawkJwt<TContext>(this IServiceCollection services)
            where 
                TContext : HawkJwtAuthProviderContext
        {
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped;

            return AddHawkJwt<TContext>(services, null, contextLifetime);

        }

        /// <summary>
        /// Add all services for using TXWV Hawk JWT Middleware.
        /// </summary>
        /// <typeparam name="TContext">Implementation class of HawkJwtAuthProviderContext</typeparam>
        /// <typeparam name="TUserIdType">UserId datatype</typeparam>
        /// <param name="services"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static IServiceCollection AddHawkJwt<TContext>(this IServiceCollection services, Action<JwtOptions> opt)
            where 
                TContext : HawkJwtAuthProviderContext
        {
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped;

            return AddHawkJwt<TContext>(services, opt, contextLifetime);
        }

        /// <summary>
        /// Add all services for using TXWV Hawk JWT Middleware.
        /// </summary>
        /// <typeparam name="TContext">Implementation class of HawkJwtAuthProviderContext</typeparam>
        /// <typeparam name="TUserIdType">UserId datatype</typeparam>
        /// <param name="services"></param>
        /// <param name="opt"></param>
        /// <param name="contextLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddHawkJwt<TContext>(
            this IServiceCollection services,
            Action<JwtOptions> opt,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped)
            where
                TContext : HawkJwtAuthProviderContext
        {
            if (opt != null)
                services.Configure<JwtOptions>(opt);

            if(contextLifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<ITokenService, TokenService>();
                services.AddScoped<IHawkJwtAuthProvider, TContext>();

            }else if(contextLifetime == ServiceLifetime.Transient)
            {
                services.AddTransient<ITokenService, TokenService>();
                services.AddTransient<IHawkJwtAuthProvider, TContext>();
            }
            else if (contextLifetime == ServiceLifetime.Singleton)
            {
                services.AddSingleton<ITokenService, TokenService>();
                services.AddSingleton<IHawkJwtAuthProvider, TContext>();
            }

            return services;
        }




    }
}



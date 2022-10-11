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
        public static IServiceCollection AddHawkJwt<TContext, TUserIdType>(this IServiceCollection services)
            where 
                TContext : HawkJwtAuthProviderContext<TUserIdType>,
                new() where TUserIdType : IConvertible
        {
            ServiceLifetime contextLifetime = ServiceLifetime.Transient;

            return AddHawkJwt<TContext, TUserIdType>(services, null, contextLifetime);

        }

        /// <summary>
        /// Add all services for using TXWV Hawk JWT Middleware.
        /// </summary>
        /// <typeparam name="TContext">Implementation class of HawkJwtAuthProviderContext</typeparam>
        /// <typeparam name="TUserIdType">UserId datatype</typeparam>
        /// <param name="services"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static IServiceCollection AddHawkJwt<TContext, TUserIdType>(this IServiceCollection services, Action<JwtOptions> opt)
            where 
                TContext : HawkJwtAuthProviderContext<TUserIdType>,
                new() where TUserIdType : IConvertible
        {
            ServiceLifetime contextLifetime = ServiceLifetime.Transient;

            return AddHawkJwt<TContext, TUserIdType>(services, opt, contextLifetime);
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
        public static IServiceCollection AddHawkJwt<TContext, TUserIdType>(
            this IServiceCollection services,
            Action<JwtOptions> opt,
            ServiceLifetime contextLifetime = ServiceLifetime.Transient)
            where
                TContext : HawkJwtAuthProviderContext<TUserIdType>,
                new() where TUserIdType : IConvertible
        {
            if (opt != null)
                services.Configure<JwtOptions>(opt);

            services.AddTransient<ITokenService<TUserIdType>, TokenService<TUserIdType>>();

            services.TryAdd(
                    new ServiceDescriptor(
                        typeof(TContext),
                        p => p.GetService<TContext>(),
                        contextLifetime));

            return services;
        }




    }
}



using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisCache.Common.Repository.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// This configuration creates a new instance of ConnectionMultiplexer
        /// </summary>
        /// <param name="services">Collection of service descriptor</param>
        /// <param name="configuration">Redis Database connection string</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, string configuration)
        {
            services.AddSingleton(provider =>
            {
                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }

        /// <summary>
        /// This configuration adds the IRedisGenericRepository and RedisGenericRepository to the service collections
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCacheRepository(this IServiceCollection services)
        {
            services.AddSingleton<ICacheCommonRepository>(provider =>
            {
                var redis = provider.GetService<ConnectionMultiplexer>();
                return new CacheCommonRepository(redis);
            });

            return services;
        }
    }
}

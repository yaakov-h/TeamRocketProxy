using System;

namespace TeamRocketProxy.Integration
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider)
            => (TService)serviceProvider.GetService(typeof(TService));
    }
}

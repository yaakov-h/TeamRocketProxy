using System;
using TeamRocketProxy.Integration.Http;
using TeamRocketProxy.Interception.Http;

namespace TeamRocketProxy.Interception
{
    sealed class InterceptionServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IHttpProxy))
            {
                return new FiddlerHttpProxy();
            }

            return null;
        }
    }
}

using System;

namespace TeamRocketProxy.Interception
{
    sealed class InterceptionServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
            => null; // TODO later for Fiddler stuff.
    }
}

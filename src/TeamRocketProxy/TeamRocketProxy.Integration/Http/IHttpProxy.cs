using System;

namespace TeamRocketProxy.Integration.Http
{
    public interface IHttpProxy
    {
        void Start(HttpProxyConfiguration configuration);
        void Stop();

        event EventHandler<HttpSessionCompleteEventArgs> OnSessionComplete;
    }
}

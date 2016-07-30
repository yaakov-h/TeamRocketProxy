using System;

namespace TeamRocketProxy.Integration.Http
{
    public interface IHttpProxy
    {
        void Start(HttpProxyConfiguration configuration);
        void Stop();

        event EventHandler<HttpSessionCompleteEventArgs> OnSessionComplete;

        void Load(string path);
        void Save(string path);
    }
}

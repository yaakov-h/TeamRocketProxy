using System;

namespace TeamRocketProxy.Integration.Http
{
    public interface IHttpProxy
    {
        void Configure(HttpProxyConfiguration configuration);

        void Start();
        void Stop();

        event EventHandler<HttpSessionCompleteEventArgs> OnSessionComplete;

        void Load(string path);
        void Save(string path);
    }
}

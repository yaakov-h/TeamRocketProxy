using System;

namespace TeamRocketProxy.Integration.Http
{
    public sealed class HttpSessionCompleteEventArgs : EventArgs
    {
        public HttpSessionCompleteEventArgs(IHttpSession session)
        {
            Session = session;
        }

        public IHttpSession Session { get; }
    }
}

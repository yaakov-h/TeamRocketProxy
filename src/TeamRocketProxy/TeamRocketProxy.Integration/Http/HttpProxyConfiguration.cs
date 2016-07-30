using System.Collections.Generic;

namespace TeamRocketProxy.Integration.Http
{
    public sealed class HttpProxyConfiguration
    {
        public HttpProxyConfiguration(int portNumber)
        {
            PortNumber = portNumber;
            hostsToIntercept = new List<string>();
        }

        readonly List<string> hostsToIntercept;

        public int PortNumber { get; set; }

        public bool DecryptSecureConnections { get; set; }

        public bool AllowRemoteConnections { get; set; }
        public IReadOnlyList<string> HostsToIntercept => hostsToIntercept.AsReadOnly();

        public void InterceptHost(string hostName)
        {
            hostsToIntercept.Add(hostName);
        }
    }
}

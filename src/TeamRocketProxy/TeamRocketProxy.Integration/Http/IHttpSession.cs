using System;
using System.IO;
using System.Net.Http;

namespace TeamRocketProxy.Integration.Http
{
    public interface IHttpSession
    {
        HttpMethod Method { get; }
        Uri Uri { get; }

        Stream GetRequestBody();
        Stream GetResponseBody();
    }
}

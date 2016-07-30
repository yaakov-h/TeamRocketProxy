using System;
using System.IO;
using System.Net.Http;
using Fiddler;
using TeamRocketProxy.Integration.Http;

namespace TeamRocketProxy.Interception.Http
{
    class FiddlerHttpSession : IHttpSession
    {
        public FiddlerHttpSession(Session session)
        {
            this.session = session;
        }

        readonly Session session;

        public HttpMethod Method
            => new HttpMethod(session.RequestMethod);

        public Uri Uri
            => new Uri(session.fullUrl, UriKind.Absolute); 

        public Stream GetRequestBody()
        {
            session.utilDecodeRequest();
            return new MemoryStream(session.RequestBody);
        }

        public Stream GetResponseBody()
        {
            session.utilDecodeResponse();
            return new MemoryStream(session.ResponseBody);
        }
    }
}

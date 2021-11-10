using System.Net;
using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Net.Http.ProtoBuf.Unit
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public ProtoBufContent ResponseContent { get; set; }

        public HttpRequestMessage Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Request = request;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = request,
                Content = ResponseContent ?? request.Content
            };
            return Task.FromResult(response);
        }
    }
}
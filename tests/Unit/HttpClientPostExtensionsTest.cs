using Byndyusoft.Net.Http.ProtoBuf.Models;
using ProtoBuf.Meta;
using System;
using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Unit
{
    public class HttpClientPostExtensionsTest
    {
        private readonly HttpClient _client;
        private readonly TypeModel _typeModel;
        private readonly string _uri = "http://localhost/";

        public HttpClientPostExtensionsTest()
        {
            _client = new HttpClient(new FakeHttpMessageHandler());
            _typeModel = RuntimeTypeModel.Default;
        }

        [Fact]
        public async Task PostAsProtoBufAsync_String_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((HttpClient)null!).PostAsProtoBufAsync(_uri, SimpleType.Create(), CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_String_WhenUriIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _client.PostAsProtoBufAsync(((string)null!), SimpleType.Create(), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_String_WhenOptionsIsNull_UsesProtoBufContentWithDefaultOptions()
        {
            var response = await _client.PostAsProtoBufAsync(_uri, SimpleType.Create(), CancellationToken.None);

            var content = Assert.IsType<ProtoBufContent>(response.RequestMessage?.Content);
            Assert.Same(ProtoBufDefaults.TypeModel, content.TypeModel);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_String_UsesProtoBufContent()
        {
            var response =
                await _client.PostAsProtoBufAsync(_uri, SimpleType.Create(), _typeModel, CancellationToken.None);

            var content = Assert.IsType<ProtoBufContent>(response.RequestMessage?.Content);
            Assert.Same(_typeModel, content.TypeModel);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_Uri_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((HttpClient)null!).PostAsProtoBufAsync(new Uri(_uri), new object(), CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_Uri_WhenUriIsNull_ThrowsException()
        {
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _client.PostAsProtoBufAsync(((Uri)null!), SimpleType.Create(), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_Uri_UsesProtoBufContent()
        {
            var response =
                await _client.PostAsProtoBufAsync(new Uri(_uri), SimpleType.Create(), _typeModel,
                    CancellationToken.None);

            var content = Assert.IsType<ProtoBufContent>(response.RequestMessage?.Content);
            Assert.Same(_typeModel, content.TypeModel);
        }

        [Fact]
        public async Task PostAsProtoBufAsync_Uri_WhenOptionsIsNull_UsesProtoBufContentWithDefaultOptions()
        {
            var response =
                await _client.PostAsProtoBufAsync(new Uri(_uri), SimpleType.Create(), CancellationToken.None);

            var content = Assert.IsType<ProtoBufContent>(response.RequestMessage?.Content);
            Assert.Same(ProtoBufDefaults.TypeModel, content.TypeModel);
        }
    }
}
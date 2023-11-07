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
    public class HttpClientGetExtensionsTest
    {
        private readonly HttpClient _client;
        private readonly FakeHttpMessageHandler _handler;
        private readonly TypeModel _typeModel;
        private readonly string _uri = "http://localhost/";

        public HttpClientGetExtensionsTest()
        {
            _handler = new FakeHttpMessageHandler();
            _client = new HttpClient(_handler);
            _typeModel = RuntimeTypeModel.Default;
        }

        [Fact]
        public async Task GetFromProtoBufAsync_StringUri_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
               ((HttpClient)null!).GetFromProtoBufAsync(_uri, typeof(object), CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_StringUri_WhenUriIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _client.GetFromProtoBufAsync(((string)null!), typeof(object), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_StringUri_Test()
        {
            _handler.ResponseContent = ProtoBufContent.Create(SimpleType.Create(), _typeModel);

            var result =
                await _client.GetFromProtoBufAsync(_uri, typeof(SimpleType), _typeModel, CancellationToken.None);

            Assert.Contains(ProtoBufDefaults.MediaTypeHeader, _handler.Request.Headers.Accept);
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_StringUri_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
               ((HttpClient)null!).GetFromProtoBufAsync<object>(_uri, CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_StringUri_WhenUriIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _client.GetFromProtoBufAsync<object>(((string)null!), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_StringUri_Test()
        {
            _handler.ResponseContent = ProtoBufContent.Create(SimpleType.Create(), _typeModel);

            var result =
                await _client.GetFromProtoBufAsync<SimpleType>(_uri, _typeModel, CancellationToken.None);

            Assert.NotNull(result);
            result.Verify();
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Uri_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
               ((HttpClient)null!).GetFromProtoBufAsync(new Uri(_uri), typeof(object), CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Uri_WhenUriIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _client.GetFromProtoBufAsync(((Uri)null!), typeof(object), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Uri_Test()
        {
            _handler.ResponseContent = ProtoBufContent.Create(SimpleType.Create(), _typeModel);

            var result =
                await _client.GetFromProtoBufAsync(new Uri(_uri), typeof(SimpleType), _typeModel,
                    CancellationToken.None);

            Assert.Contains(ProtoBufDefaults.MediaTypeHeader, _handler.Request.Headers.Accept);
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_Uri_WhenClientIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
               ((HttpClient)null!).GetFromProtoBufAsync<object>(new Uri(_uri), CancellationToken.None));
            Assert.Equal("client", exception.ParamName);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_Uri_WhenUriIsNull_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _client.GetFromProtoBufAsync<object>(((Uri)null!), CancellationToken.None));
            Assert.Equal(
                "An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.",
                exception.Message);
        }

        [Fact]
        public async Task GetFromProtoBufAsync_Generic_Uri_Test()
        {
            _handler.ResponseContent = ProtoBufContent.Create(SimpleType.Create(), _typeModel);

            var result =
                await _client.GetFromProtoBufAsync<SimpleType>(new Uri(_uri), _typeModel, CancellationToken.None);

            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }
    }
}
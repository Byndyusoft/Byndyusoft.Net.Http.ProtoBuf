﻿using Byndyusoft.Net.Http.ProtoBuf.Models;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;
using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Functional
{
    public class HttpClientExtensionsTest : MvcTestFixture
    {
        private readonly TypeModel _typeModel;

        public HttpClientExtensionsTest()
        {
            _typeModel = RuntimeTypeModel.Default;
        }

        protected override void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder.AddProtoBufNet(
                options => { options.Model = _typeModel; });
        }

        protected override void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(ProtoBufDefaults.MediaTypeHeader);
        }

        [Fact]
        public async Task PostAsProtoBufAsync()
        {
            // Arrange
            var input = SimpleType.Create();

            // Act
            var response = await Client.PostAsProtoBufAsync("/protobuf-formatter", input, _typeModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromProtoBufAsync<SimpleType>();

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }

        [Fact]
        public async Task PutAsProtoBufAsync()
        {
            // Arrange
            var input = SimpleType.Create();

            // Act
            var response = await Client.PutAsProtoBufAsync("/protobuf-formatter", input, _typeModel);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromProtoBufAsync<SimpleType>();

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);

            model.Verify();
        }

        [Fact]
        public async Task GetFromProtoBufAsync()
        {
            // Act
            var result = await Client.GetFromProtoBufAsync<SimpleType>("/protobuf-formatter", _typeModel);

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);

            model.Verify();
        }
    }
}
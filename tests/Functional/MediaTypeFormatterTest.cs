using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Byndyusoft.Net.Http.ProtoBuf.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Functional
{
    public class MediaTypeFormatterTest : MvcTestFixture
    {
        private readonly ProtoBufMediaTypeFormatter _formatter;

        public MediaTypeFormatterTest()
        {
            _formatter = new ProtoBufMediaTypeFormatter();
        }

        protected override void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(ProtoBufDefaults.MediaTypeHeader);
        }

        protected override void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder.AddProtoBufNet(
                options => { options.Model = ProtoBufDefaults.TypeModel; });
        }

        [Fact]
        public async Task Test()
        {
            // Arrange
            var input = SimpleType.Create();

            // Act
            var content = new ObjectContent<SimpleType>(input, _formatter);
            var response = await Client.PostAsync("/protobuf-formatter", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<SimpleType>(new[] {_formatter});

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }
    }
}
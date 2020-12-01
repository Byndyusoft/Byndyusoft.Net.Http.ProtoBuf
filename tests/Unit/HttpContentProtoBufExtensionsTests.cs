using System;
using System.Net.Http;
using System.Net.Http.ProtoBuf;
using System.Threading.Tasks;
using Byndyusoft.Net.Http.ProtoBuf.Models;
using ProtoBuf.Meta;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Unit
{
    public class HttpContentProtoBufExtensionsTests
    {
        private readonly TypeModel _typeModel = RuntimeTypeModel.Default;

        [Fact]
        public async Task ReadFromProtoBufAsync_NullContent_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((HttpContent) null).ReadFromProtoBufAsync(typeof(object)));

            Assert.Equal("content", exception.ParamName);
        }

        [Fact]
        public async Task ReadFromProtoBufAsync_Generic_NullContent_ThrowsException()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                ((HttpContent) null).ReadFromProtoBufAsync<object>());

            Assert.Equal("content", exception.ParamName);
        }

        [Fact]
        public async Task ReadFromProtoBufAsync_Test()
        {
            var content = new StreamProtoBufHttpContent();
            content.WriteObject(SimpleType.Create(), _typeModel);

            var model = await content.ReadFromProtoBufAsync(typeof(SimpleType), _typeModel);

            var simpleType = Assert.IsType<SimpleType>(model);
            simpleType.Verify();
        }

        [Fact]
        public async Task ReadFromProtoBufAsync_NullObject_Test()
        {
            var content = new StreamProtoBufHttpContent();
            content.WriteObject<ValueMember>(null, _typeModel);

            var model = await content.ReadFromProtoBufAsync(typeof(SimpleType), _typeModel);

            Assert.Null(model);
        }

        [Fact]
        public async Task ReadFromProtoBufAsync_Generic_Test()
        {
            var content = new StreamProtoBufHttpContent();
            content.WriteObject(SimpleType.Create(), _typeModel);

            var model = await content.ReadFromProtoBufAsync<SimpleType>(_typeModel);

            model.Verify();
        }

        [Fact]
        public async Task ReadFromProtoBufAsync_Generic_NullObject_Test()
        {
            var content = new StreamProtoBufHttpContent();
            content.WriteObject<ValueMember>(null, _typeModel);

            var model = await content.ReadFromProtoBufAsync<SimpleType>(_typeModel);

            Assert.Null(model);
        }
    }
}
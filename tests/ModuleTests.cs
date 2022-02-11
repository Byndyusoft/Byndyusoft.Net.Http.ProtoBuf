#if NET5_0_OR_GREATER

using System.Net.Http.Formatting;
using System.Net.Http.ProtoBuf;
using System.Net.Http.ProtoBuf.Formatting;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf
{
    public class ModuleTests
    {
        [Fact]
        public void Init_Test()
        {
            // assert
            var writer = MediaTypeFormatterCollection.Default.FindWriter(typeof(string), ProtoBufDefaults.MediaTypeHeader);
            Assert.NotNull(writer);
            Assert.IsType<ProtoBufMediaTypeFormatter>(writer);
            
            var reader = MediaTypeFormatterCollection.Default.FindReader(typeof(string), ProtoBufDefaults.MediaTypeHeader);
            Assert.NotNull(reader);
            Assert.IsType<ProtoBufMediaTypeFormatter>(reader);
        }
    }
}

#endif
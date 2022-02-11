using ProtoBuf;
using Xunit;

namespace Byndyusoft.Net.Http.ProtoBuf.Models
{
    [ProtoContract]
    public class ComplexType
    {
        [ProtoMember(1)] public SimpleType Inner { get; set; } = default!;

        public static ComplexType Create()
        {
            return new ComplexType
            {
                Inner = SimpleType.Create()
            };
        }

        public void Verify()
        {
            Assert.NotNull(Inner);
            Inner.Verify();
        }
    }
}
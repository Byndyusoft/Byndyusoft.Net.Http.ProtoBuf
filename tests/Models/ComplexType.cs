using ProtoBuf;

namespace Byndyusoft.Net.Http.ProtoBuf.Models
{
    [ProtoContract]
    public class ComplexType
    {
        [ProtoMember(1)] public SimpleType Inner { get; set; }
    }
}
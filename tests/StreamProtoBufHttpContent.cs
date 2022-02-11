using ProtoBuf.Meta;
using System.IO;
using System.Net.Http;

namespace Byndyusoft.Net.Http.ProtoBuf
{
    public class StreamProtoBufHttpContent : StreamContent
    {
        public StreamProtoBufHttpContent() : this(new MemoryStream())
        {
        }

        private StreamProtoBufHttpContent(MemoryStream stream) : base(stream)
        {
            Stream = stream;
        }

        private MemoryStream Stream { get; }

        public void WriteObject<T>(T value, TypeModel typeModel)
        {
            if (value != null) typeModel.Serialize(Stream, value);
            Stream.Position = 0;
        }

        public T ReadObject<T>(TypeModel typeModel)
        {
            if (Stream.Length == 0)
                return default!;

            Stream.Position = 0;
            return typeModel.Deserialize<T>(Stream);
        }
    }
}
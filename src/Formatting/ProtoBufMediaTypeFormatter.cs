using ProtoBuf.Meta;
using System.IO;
using System.Net.Http.Formatting;
using System.Threading;

namespace System.Net.Http.ProtoBuf.Formatting
{
    /// <summary>
    ///     <see cref="MediaTypeFormatter" /> class to handle ProtoBuf.
    /// </summary>
    /// <see
    ///     href="https://github.com/protobuf-net/protobuf-net/blob/main/src/protobuf-net.AspNetCore/Formatters/ProtoInputFormatter.cs" />
    public class ProtoBufMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProtoBufMediaTypeFormatter" /> class.
        /// </summary>
        /// <param name="model">Options for running serialization.</param>
        public ProtoBufMediaTypeFormatter(TypeModel? model = null)
        {
            TypeModel = model ?? ProtoBufDefaults.TypeModel;
            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypeHeaders.ApplicationProtoBuf);
            SupportedMediaTypes.Add(ProtoBufDefaults.MediaTypeHeaders.ApplicationXProtoBuf);
        }

        /// <summary>
        ///     Provides protobuf serialization support for a number of types.
        /// </summary>
        public TypeModel TypeModel { get; }

        /// <inheritdoc />
        public override object? ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger? formatterLogger = null,
            CancellationToken cancellationToken = default)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (readStream is null) throw new ArgumentNullException(nameof(readStream));
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (content is ObjectContent objectContent) return objectContent.Value;

            var length = content.Headers.ContentLength ?? -1;
            if (length == 0)
                return null;

            return TypeModel.Deserialize(readStream, null, type);
        }

        /// <inheritdoc />
        public override void WriteToStream(Type type, object? value, Stream writeStream, HttpContent content,
            CancellationToken cancellationToken = default)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (writeStream is null) throw new ArgumentNullException(nameof(writeStream));
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (value != null)
            {
                TypeModel.Serialize(writeStream, value);
            }
        }

        /// <inheritdoc />
        public override bool CanReadType(Type type) => CanSerialize(type);

        /// <inheritdoc />
        public override bool CanWriteType(Type type) => CanSerialize(type);

        private bool CanSerialize(Type type) => TypeModel.CanSerialize(type);
    }
}
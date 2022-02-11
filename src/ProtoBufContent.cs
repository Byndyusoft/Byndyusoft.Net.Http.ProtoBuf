using ProtoBuf.Meta;
using System.Net.Http.Headers;
using System.Net.Http.ProtoBuf.Formatting;

namespace System.Net.Http.ProtoBuf
{
    /// <summary>
    ///     Provides HTTP content based on ProtoBuf.
    /// </summary>
    public sealed class ProtoBufContent : ObjectContent
    {
        private ProtoBufContent(Type type, object? value, ProtoBufMediaTypeFormatter formatter,
            MediaTypeHeaderValue? mediaType)
            : base(type, value, formatter, mediaType)
        {
            TypeModel = formatter.TypeModel;
        }

        /// <summary>
        ///     Options to control the behavior during serialization.
        /// </summary>
        public TypeModel TypeModel { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="ProtoBufContent" /> class that will contain the
        ///     <see cref="value" />  serialized as ProtoBuf.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="typeModel">Options to control the behavior during serialization.</param>
        /// <param name="mediaType">The media type to use for the content.</param>
        /// <returns>A <see cref="ProtoBufContent" /> instance.</returns>
        public static ProtoBufContent Create<T>(T value,
            TypeModel? typeModel = null, MediaTypeHeaderValue? mediaType = null)
        {
            return Create(typeof(T), value, typeModel, mediaType);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ProtoBufContent" /> class that will contain the
        ///     <see cref="value" />  serialized as ProtoBuf.
        /// </summary>
        /// <param name="type">The type of the value to serialize.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="typeModel">Options to control the behavior during serialization.</param>
        /// <param name="mediaType">The media type to use for the content.</param>
        /// <returns>A <see cref="ProtoBufContent" /> instance.</returns>
        public static ProtoBufContent Create(Type type,
            object? value, TypeModel? typeModel = null, MediaTypeHeaderValue? mediaType = null)
        {
            var formatter = new ProtoBufMediaTypeFormatter(typeModel);

            return new ProtoBufContent(type, value, formatter, mediaType);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = TypeModel.Measure(Value).LengthOnly();
            return true;
        }
    }
}
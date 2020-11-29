using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Meta;

namespace System.Net.Http.ProtoBuf
{
    /// <summary>
    ///     Provides HTTP content based on ProtoBuf.
    /// </summary>
    public sealed class ProtoBufContent : HttpContent
    {
        private MeasureState<object>? _measureState;

        private ProtoBufContent(object inputValue, Type inputType, MediaTypeHeaderValue mediaType, TypeModel model)
        {
            if (inputType == null) throw new ArgumentNullException(nameof(inputType));

            if (inputValue != null && !inputType.IsInstanceOfType(inputValue))
                throw new ArgumentException(
                    $"The specified type {inputType} must derive from the specific value's type {inputValue.GetType()}.");

            Value = inputValue;
            ObjectType = inputType;
            Headers.ContentType = mediaType ?? ProtoBufDefaults.MediaTypeHeader;
            TypeModel = model ?? ProtoBufDefaults.TypeModel;
        }

        /// <summary>
        ///     Options to control the behavior during serialization.
        /// </summary>
        public TypeModel TypeModel { get; }

        /// <summary>
        ///     Gets the type of the <see cref="Value" />  to be serialized by this instance.
        /// </summary>
        public Type ObjectType { get; }

        /// <summary>
        ///     Gets the value to be serialized and used as the body of the HttpRequestMessage that sends this instance.
        /// </summary>
        public object Value { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="ProtoBufContent" /> class that will contain the
        ///     <see cref="inputValue" />  serialized as ProtoBuf.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="inputValue">The value to serialize.</param>
        /// <param name="typeModel">Options to control the behavior during serialization.</param>
        /// <param name="mediaType">The media type to use for the content.</param>
        /// <returns>A <see cref="ProtoBufContent" /> instance.</returns>
        public static ProtoBufContent Create<T>(T inputValue,
            TypeModel typeModel = null, MediaTypeHeaderValue mediaType = null)
        {
            return Create(inputValue, typeof(T), typeModel, mediaType);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ProtoBufContent" /> class that will contain the
        ///     <see cref="inputValue" />  serialized as ProtoBuf.
        /// </summary>
        /// <param name="inputValue">The value to serialize.</param>
        /// <param name="inputType">The type of the value to serialize.</param>
        /// <param name="typeModel">Options to control the behavior during serialization.</param>
        /// <param name="mediaType">The media type to use for the content.</param>
        /// <returns>A <see cref="ProtoBufContent" /> instance.</returns>
        public static ProtoBufContent Create(object inputValue, Type inputType,
            TypeModel typeModel = null,
            MediaTypeHeaderValue mediaType = null)
        {
            return new ProtoBufContent(inputValue, inputType, mediaType, typeModel);
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            if (Value != null)
            {
                var measureState = Measure();
                measureState.Serialize(stream);
            }

            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing || _measureState == null)
                return;

            _measureState.Value.Dispose();
            _measureState = null;
        }

        protected override bool TryComputeLength(out long length)
        {
            var measureState = Measure();
            length = measureState.Length;
            return true;
        }

        private MeasureState<object> Measure()
        {
            if (_measureState == null) _measureState = TypeModel.Measure(Value);

            return _measureState.Value;
        }
    }
}
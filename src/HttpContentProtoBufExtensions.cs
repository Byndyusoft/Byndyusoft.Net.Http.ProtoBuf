using System.Net.Http.ProtoBuf.Internal;
using System.Threading;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace System.Net.Http.ProtoBuf
{
    /// <summary>
    ///     Contains extension methods to read and then parse the <see cref="HttpContent" /> from ProtoBuf.
    /// </summary>
    public static class HttpContentProtoBufExtensions
    {
        /// <summary>
        ///     Reads the HTTP content and returns the value that results from deserializing the content as ProtoBuf in an
        ///     asynchronous operation.
        /// </summary>
        /// <param name="content">The content to read from.</param>
        /// <param name="type">The type of the object to deserialize to and return.</param>
        /// <param name="typeModel">Options to control the behavior during deserialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<object> ReadFromProtoBufAsync(this HttpContent content, Type type,
            TypeModel typeModel = null, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (content is ProtoBufContent protoBufContent) return protoBufContent.Value;

            return await ProtoBufHelper.ReadFromStreamAsync(content, type, typeModel, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Reads the HTTP content and returns the value that results from deserializing the content as ProtoBuf in an
        ///     asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The target type to deserialize to.</typeparam>
        /// <param name="content">The content to read from.</param>
        /// <param name="typeModel">Options to control the behavior during deserialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<T> ReadFromProtoBufAsync<T>(this HttpContent content,
            TypeModel typeModel = null, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return (T) await ReadFromProtoBufAsync(content, typeof(T), typeModel, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
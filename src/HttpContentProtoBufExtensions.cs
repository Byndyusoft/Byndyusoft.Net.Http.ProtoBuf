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
        public static Task<object> ReadFromProtoBufAsync(this HttpContent content, Type type,
            TypeModel typeModel = null, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return ReadFromProtoBufAsyncCore(content, type, typeModel, cancellationToken);
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
        public static Task<T> ReadFromProtoBufAsync<T>(this HttpContent content,
            TypeModel typeModel = null, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return ReadFromProtoBufAsyncCore<T>(content, typeModel, cancellationToken);
        }

        private static async Task<object> ReadFromProtoBufAsyncCore(HttpContent content, Type type,
            TypeModel typeModel, CancellationToken cancellationToken)
        {
            if (typeModel == null) typeModel = RuntimeTypeModel.Default;

            using (var stream = await content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                return stream.Length != 0 ? typeModel.Deserialize(stream, null, type) : default;
            }
        }

        private static async Task<T> ReadFromProtoBufAsyncCore<T>(HttpContent content,
            TypeModel typeModel, CancellationToken cancellationToken)
        {
            if (typeModel == null) typeModel = RuntimeTypeModel.Default;

            using (var stream = await content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();
                return stream.Length != 0 ? typeModel.Deserialize<T>(stream) : default;
            }
        }
    }
}
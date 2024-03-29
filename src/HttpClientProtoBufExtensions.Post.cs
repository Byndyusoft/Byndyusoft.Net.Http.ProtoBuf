using ProtoBuf.Meta;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.ProtoBuf
{
    /// <summary>
    ///     Contains the extensions methods for using ProtoBuf as the content-type in HttpClient.
    /// </summary>
    public static partial class HttpClientProtoBufExtensions
    {
        /// <summary>
        ///     Send a POST request to the specified Uri containing the <paramref name="value" /> serialized as ProtoBuf in the
        ///     request body.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsProtoBufAsync<TValue>(this HttpClient client,
            string requestUri, TValue value, TypeModel? typeModel = null,
            CancellationToken cancellationToken = default)
        {
            Guard.NotNull(client,nameof(client));

            var content = ProtoBufContent.Create(value, typeModel);
            return client.PostAsync(requestUri, content, cancellationToken);
        }

        /// <summary>
        ///     Send a POST request to the specified Uri containing the <paramref name="value" /> serialized as ProtoBuf in the
        ///     request body.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsProtoBufAsync<TValue>(this HttpClient client, Uri requestUri,
            TValue value, TypeModel? typeModel = null, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(client,nameof(client));

            var content = ProtoBufContent.Create(value, typeModel);
            return client.PostAsync(requestUri, content, cancellationToken);
        }

        /// <summary>
        ///     Send a POST request to the specified Uri containing the <paramref name="value" /> serialized as ProtoBuf in the
        ///     request body.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsProtoBufAsync<TValue>(this HttpClient client,
            string requestUri, TValue value, CancellationToken cancellationToken)
        {
            Guard.NotNull(client,nameof(client));

            return client.PostAsProtoBufAsync(requestUri, value, null, cancellationToken);
        }

        /// <summary>
        ///     Send a POST request to the specified Uri containing the <paramref name="value" /> serialized as ProtoBuf in the
        ///     request body.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsProtoBufAsync<TValue>(this HttpClient client, Uri requestUri,
            TValue value, CancellationToken cancellationToken)
        {
            Guard.NotNull(client,nameof(client));

            return client.PostAsProtoBufAsync(requestUri, value, null, cancellationToken);
        }
    }
}
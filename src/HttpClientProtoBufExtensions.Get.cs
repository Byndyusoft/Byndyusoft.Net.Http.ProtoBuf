using System.Threading;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace System.Net.Http.ProtoBuf
{
    /// <summary>
    ///     Contains the extensions methods for using ProtoBuf as the content-type in HttpClient.
    /// </summary>
    public static partial class HttpClientProtoBufExtensions
    {
        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="type">The type of the object to deserialize to and return.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<object> GetFromProtoBufAsync(this HttpClient client, string requestUri, Type type,
            TypeModel typeModel, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Accept.Add(ProtoBufDefaults.MediaTypeHeader);

            var taskResponse = client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead,
                cancellationToken);
            return GetFromProtoBufAsyncCore(taskResponse, type, typeModel, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="type">The type of the object to deserialize to and return.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<object> GetFromProtoBufAsync(this HttpClient client, Uri requestUri, Type type,
            TypeModel typeModel, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Accept.Add(ProtoBufDefaults.MediaTypeHeader);

            var taskResponse = client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead,
                cancellationToken);
            return GetFromProtoBufAsyncCore(taskResponse, type, typeModel, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<TValue> GetFromProtoBufAsync<TValue>(this HttpClient client, string requestUri,
            TypeModel typeModel, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Accept.Add(ProtoBufDefaults.MediaTypeHeader);

            var taskResponse = client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead,
                cancellationToken);
            return GetFromProtoBufAsyncCore<TValue>(taskResponse, typeModel, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="typeModel">Options for running the serialization.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<TValue> GetFromProtoBufAsync<TValue>(this HttpClient client, Uri requestUri,
            TypeModel typeModel, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Accept.Add(ProtoBufDefaults.MediaTypeHeader);

            var taskResponse = client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead,
                cancellationToken);
            return GetFromProtoBufAsyncCore<TValue>(taskResponse, typeModel, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="type">The type of the object to deserialize and return to.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<object> GetFromProtoBufAsync(this HttpClient client, string requestUri, Type type,
            CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            return client.GetFromProtoBufAsync(requestUri, type, null, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="type">The type of the object to deserialize and return to.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<object> GetFromProtoBufAsync(this HttpClient client, Uri requestUri, Type type,
            CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            return client.GetFromProtoBufAsync(requestUri, type, null, cancellationToken);
        }

        /// <summary>
        ///     Sends a GET request to the specified Uri and returns the value that results from deserializing the response body as
        ///     ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<TValue> GetFromProtoBufAsync<TValue>(this HttpClient client, string requestUri,
            CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            return client.GetFromProtoBufAsync<TValue>(requestUri, null, cancellationToken);
        }

        /// <summary>
        ///     Send a GET request  to the specified Uri and return the value resulting from deserialize the response body
        ///     as ProtoBuf in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<TValue> GetFromProtoBufAsync<TValue>(this HttpClient client, Uri requestUri,
            CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            return client.GetFromProtoBufAsync<TValue>(requestUri, null, cancellationToken);
        }

        private static async Task<object> GetFromProtoBufAsyncCore(Task<HttpResponseMessage> taskResponse, Type type,
            TypeModel typeModel, CancellationToken cancellationToken)
        {
            using (var response = await taskResponse.ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromProtoBufAsync(type, typeModel, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        private static async Task<T> GetFromProtoBufAsyncCore<T>(Task<HttpResponseMessage> taskResponse,
            TypeModel typeModel, CancellationToken cancellationToken)
        {
            using (var response = await taskResponse.ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromProtoBufAsync<T>(typeModel, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
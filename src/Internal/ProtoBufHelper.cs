using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using ProtoBuf.Meta;

namespace System.Net.Http.ProtoBuf.Internal
{
    /// <see
    ///     href="https://github.com/protobuf-net/protobuf-net/blob/main/src/protobuf-net.AspNetCore/Formatters/ProtoInputFormatter.cs" />
    public static class ProtoBufHelper
    {
        public static async Task<object> ReadFromStreamAsync(HttpContent content, Type type, TypeModel typeModel,
            CancellationToken cancellationToken)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (content == null) throw new ArgumentNullException(nameof(content));

            typeModel ??= ProtoBufDefaults.TypeModel;
            var readStream = await content.ReadAsStreamAsync().ConfigureAwait(false);

            var length = content.Headers.ContentLength ?? -1;
            if (length == 0)
                return null;

            if (length == -1)
            {
                if (readStream.CanSeek) await ReadDirectStreamAsync(typeModel, type, readStream).ConfigureAwait(false);

                return await ReadBufferedStreamAsync(typeModel, type, readStream, cancellationToken)
                    .ConfigureAwait(false);
            }

            var reader = PipeReader.Create(readStream);

            // try and read synchronously
            if (reader.TryRead(out var readResult) &&
                ProcessReadBuffer(typeModel, type, reader, length, readResult, out var payload))
                return payload;

            return await ReadRequestBodyAsyncSlow(typeModel, type, reader, length, cancellationToken)
                .ConfigureAwait(false);
        }

        private static Task<object> ReadDirectStreamAsync(TypeModel typeModel, Type type, Stream stream)
        {
            var model = typeModel.Deserialize(stream, null, type);
            return Task.FromResult(model);
        }

        private static async Task<object> ReadBufferedStreamAsync(TypeModel typeModel, Type type, Stream stream,
            CancellationToken cancellationToken)
        {
            using var readStream = new FileBufferingReadStream(stream, 256 * 1024, null, Path.GetTempPath);
            await readStream.DrainAsync(cancellationToken).ConfigureAwait(false);
            readStream.Position = 0;
            var payload = typeModel.Deserialize(readStream, null, type);
            return payload;
        }

        private static bool ProcessReadBuffer(TypeModel typeModel, Type type, PipeReader reader, long length,
            in ReadResult readResult, out object payload)
        {
            if (readResult.IsCanceled) throw new OperationCanceledException();
            var buffer = readResult.Buffer;
            if (ParseIfComplete(typeModel, type, ref buffer, length, out payload))
            {
                // mark consumed
                reader.AdvanceTo(buffer.End, buffer.End);
                return true;
            }

            if (readResult.IsCompleted)
                throw new InvalidOperationException(
                    $"Incomplete protobuf payload received; got {buffer.Length} of {length} bytes");

            // not enough data; put it back by saying we looked at everything and took nothing
            reader.AdvanceTo(buffer.Start, buffer.End);
            return false;
        }

        private static async Task<object> ReadRequestBodyAsyncSlow(TypeModel typeModel, Type type, PipeReader reader,
            long length, CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var readResult = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
                if (ProcessReadBuffer(typeModel, type, reader, length, readResult, out var payload)) return payload;
            }
        }

        private static bool ParseIfComplete(TypeModel typeModel, Type type, ref ReadOnlySequence<byte> buffer,
            long expectedLength, out object payload)
        {
            var availableLength = buffer.Length;
            if (availableLength < expectedLength)
            {
                // not enough (yet, we can retry later)
                payload = default;
                return false;
            }

            if (availableLength > expectedLength)
                // too much; cut it down to size
                buffer = buffer.Slice(0, expectedLength);

            payload = typeModel.Deserialize(buffer, type: type);
            return true;
        }
    }
}
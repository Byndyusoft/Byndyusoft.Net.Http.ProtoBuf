using ProtoBuf.Meta;
using System.Net.Http.Headers;

namespace System.Net.Http.ProtoBuf
{
    public static class ProtoBufDefaults
    {
        public static readonly string MediaTypeFormat = "protobuf";

        public static readonly string MediaType = MediaTypes.ApplicationProtoBuf;

        public static readonly MediaTypeWithQualityHeaderValue MediaTypeHeader =
            new MediaTypeWithQualityHeaderValue(MediaType);

        public static readonly TypeModel TypeModel = RuntimeTypeModel.Default;

        public static class MediaTypeHeaders
        {
            public static readonly MediaTypeWithQualityHeaderValue ApplicationProtoBuf =
                new MediaTypeWithQualityHeaderValue(MediaTypes.ApplicationProtoBuf);

            public static readonly MediaTypeWithQualityHeaderValue ApplicationXProtoBuf =
                new MediaTypeWithQualityHeaderValue(MediaTypes.ApplicationXProtoBuf);
        }

        public static class MediaTypes
        {
            public const string ApplicationProtoBuf = "application/protobuf";
            public const string ApplicationXProtoBuf = "application/x-protobuf";
        }
    }
}
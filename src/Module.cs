#if NET5_0_OR_GREATER

using System.Net.Http.Formatting;
using System.Net.Http.ProtoBuf.Formatting;
using System.Runtime.CompilerServices;

namespace System.Net.Http.ProtoBuf
{
    internal static class Module
    {
        [ModuleInitializer]
        internal static void Init()
        {
            MediaTypeFormatterCollection.Default.Add(new ProtoBufMediaTypeFormatter());
        }
    }
}

#endif
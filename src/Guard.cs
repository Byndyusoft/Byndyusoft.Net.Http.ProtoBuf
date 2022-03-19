namespace System.Net.Http.ProtoBuf
{
    internal static class Guard
    {
        public static void NotNull<T>(T value, string paramName)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
        }
    }
}
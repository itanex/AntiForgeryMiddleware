using System;

namespace AntiForgeryMiddleware.Exceptions
{
    public sealed class UnsupportedHttpMethod : Exception
    {
        public UnsupportedHttpMethod()
        {
        }

        public UnsupportedHttpMethod(string message)
            : base(message)
        {
        }

        public UnsupportedHttpMethod(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

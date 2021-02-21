using System;

namespace AqueductExample.Shared.Exceptions
{
    public class SharedException : Exception
    {
        public SharedException(string message) : base(message)
        {
        }
    }
}
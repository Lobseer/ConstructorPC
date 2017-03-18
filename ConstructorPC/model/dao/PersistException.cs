using System;

namespace ConstructorPC
{
    class PersistException : Exception
    {
        public PersistException() : base() { }
        public PersistException(string message) : base(message) { }
        public PersistException(string message, Exception innerException) : base(message, innerException) { }
    }
}

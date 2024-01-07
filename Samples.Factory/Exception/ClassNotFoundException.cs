using System.Runtime.Serialization;

namespace Samples.Factory.Exception
{
    public class ClassNotFoundException : System.Exception
    {
        public ClassNotFoundException()
        {
        }

        public ClassNotFoundException(string? message) : base(message)
        {
        }

        public ClassNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected ClassNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
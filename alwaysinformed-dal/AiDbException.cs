using System.Runtime.Serialization;

namespace alwaysinformed_dal
{
    [Serializable]
    internal class AiDbException : Exception
    {
        public AiDbException()
        {
        }

        public AiDbException(string? message) : base(message)
        {
        }

        public AiDbException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AiDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
namespace alwaysinformed.Validation;
public class APIException : Exception
{
	public APIException() { }
	public APIException(string message) : base(message) { }
	public APIException(string message, Exception inner) : base(message, inner) { }
	protected APIException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Validation
{

	[Serializable]
	public class WebException : Exception
	{
		public WebException() { }
		public WebException(string message) : base(message) { }
		public WebException(string message, Exception inner) : base(message, inner) { }
		protected WebException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

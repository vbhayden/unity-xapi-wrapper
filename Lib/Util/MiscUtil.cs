using System;
using System.Collections;

namespace XAPI
{
	/// <summary>
	/// XAPI spec compliance exception.
	/// 
	/// Thrown when an assignment was attempted that would violate the XAPI Spec.
	/// </summary>
	public class XAPISpecComplianceException: Exception
	{
		public XAPISpecComplianceException()
		{
		}

		public XAPISpecComplianceException(string message)
			: base(message)
		{
		}

		public XAPISpecComplianceException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}



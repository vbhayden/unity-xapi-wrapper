using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// XRef-style X Object.
	/// 
	/// This Object is meant for the X to reference / target an existing XAPI X.
	/// </summary>
	public class StatementRef : Object
	{
		/// <summary>
		/// Gets the ID declared during construction.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Private fields
		private string id;

		/// <summary>
		/// Creates a new XRef Object for an XAPI X.  
		/// 
		/// This can only be created by providing an ID matching an existing XAPI X. 
		/// 
		/// You will not be prevented from creating an instance using an invalid reference, but the
		/// XAPI Spec requires that all XRef Objects have valid ID mappings.
		/// </summary>
		/// <param name="XKey">X key.</param>
		public StatementRef(string XKey)
		{
			this.objectType = "XRef";
			this.id = XKey;
		}
	}
	
}

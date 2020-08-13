using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;
using UnityEngine;

namespace XAPI
{
	/// <summary>
	/// Base class for Actor, Statement, and Activity.
	/// 
	/// Statement objects can be Actors, other Statements, or Activities.  Those classes inherit from
	/// this to simplify the process of creating statements.
	/// </summary>
	[System.Serializable]
	public class Object
	{
		/// <summary>
		/// Gets the ID declared during construction.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("objectType", NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		protected string objectType;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.Object"/> class.
		/// 
		/// This is intended only for JSON, do not use manually.
		/// </summary>
		[JsonConstructor]
		public Object()
		{
			
		}
	}
}


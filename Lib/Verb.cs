using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// Class representing the Verb component of an XAPI Statement.
	/// </summary>
	[System.Serializable]
	public class Verb
	{
		/// <summary>
		/// Gets the name of this verb.
		/// </summary>
		/// <value>The name.</value>
		[JsonIgnore]
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		/// Gets the unique ID for this verb.
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

		/// <summary>
		/// Gets the pairs of localization ID's and the corresponding local representation of the verb.
		/// 
		/// Returns null if none exist.
		/// </summary>
		/// <value>The local display.</value>
		[JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> LocalDisplay
		{
			get
			{
				if (this.localDisplay == null || this.localDisplay.Count == 0)
					return null;

				return this.localDisplay;
			}
		}

		// Private fields
		private string name = "";
		private string id = "";
		private Dictionary<string, string> localDisplay;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.Verb"/> class.
		/// </summary>
		public Verb(string name, string id)
		{
			this.name = name;
			this.id = id;
			this.localDisplay = new Dictionary<string, string>();
		}

		/// <summary>
		/// Adds a display pair to this verb.
		/// </summary>
		/// <param name="localKey">Local key.</param>
		/// <param name="localRepresentation">Local representation.</param>
		public void AddDisplayPair(string localKey, string localRepresentation)
		{
			if (this.localDisplay == null)
				this.localDisplay = new Dictionary<string, string>();

			this.localDisplay.Add(localKey, localRepresentation);
		}
	}
}


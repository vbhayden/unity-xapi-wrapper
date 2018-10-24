using System;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// Activity-style X Object.
	/// </summary>
    [System.Serializable]
	public class XActivity : XObject
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

		/// <summary>
		/// Gets the meta data associated with this activity.
		/// 
		/// This is null by default and must be assigned.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("definition", NullValueHandling = NullValueHandling.Ignore)]
		public XActivityMetaData MetaData
		{
			get
			{
				return this.metaData;
			}
			set
			{
				this.metaData = value;
			}
		}

		// Private fields
		private string id;
		private XActivityMetaData metaData;

		/// <summary>
		/// Creates a new Activity instance from the given ID.
		/// </summary>
		/// <param name="id">Identifier IRI.</param>
		public XActivity(string id = "http://activity.com/id", string name = null)
		{
			this.objectType = "Activity";
			this.id = id;

            // Assign the name if necessary
            if (name != null)
            {
                this.metaData = new XActivityMetaData();
                this.metaData.AddNamePair("en-US", name);
            }
		}
	}

	/// <summary>
	/// Activity meta data related to an Activity.
	/// 
	/// This object is an optional parameter on Activity-Type XObjects.
	/// 
	/// !NOTE: "Extensions" have not yet not been implemented.
	/// </summary>
	[System.Serializable]
	public class XActivityMetaData
	{
		/// <summary>
		/// Mapping of name pairs.  Locale : Local Description.
		/// 
		/// !Note: Do not use this to add key pairs manually.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> NamePairs
		{
			get
			{
				if (this.namePairs == null || this.namePairs.Count == 0)
					return null;

				return this.namePairs;
			}
		}

		/// <summary>
		/// Mapping of name pairs.  Locale : Local Description.
		/// 
		/// !Note: Do not use this to add key pairs manually.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> DescriptionPairs
		{
			get
			{
				if (this.descriptionPairs == null || this.descriptionPairs.Count == 0)
					return null;

				return this.descriptionPairs;
			}
		}

		/// <summary>
		/// (IRI) Gets the type of the activity.
		/// </summary>
		/// <value>The type of the activity.</value>
		[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
		public string ActivityType
		{
			get
			{
				return this.activityType;
			}
		}

		/// <summary>
		/// (IRL) Resolves to a document with human-readable information about the Activity, which could include a way to launch the activity.
		/// </summary>
		/// <value>The more info.</value>
		[JsonProperty("moreInfo", NullValueHandling = NullValueHandling.Ignore)]
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Private Fields
		private Dictionary<string, string> namePairs;
		private Dictionary<string, string> descriptionPairs;
		private string activityType;
		private string moreInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.ActivityMetaData"/> class.
		/// </summary>
		/// <param name="activityType">(IRI) Activity type.</param>
		/// <param name="moreInfo">(IRL) Resolves to a document with human-readable information about the Activity, which could include a way to launch the activity.</param>
		public XActivityMetaData(string activityType = null, string moreInfo = null)
		{
			this.activityType = activityType;
			this.moreInfo = moreInfo;

			// We may not need these
			this.namePairs = new Dictionary<string, string>();
			this.descriptionPairs = new Dictionary<string, string>();
		}

        /// <summary>
        /// Check if the metadata instance has a given key.
        /// </summary>
        /// <param name="localKey"></param>
        public bool HasNameKey(string localKey)
        {
            return this.namePairs.ContainsKey(localKey);
        }

		/// <summary>
		/// Adds the name pair.
		/// </summary>
		/// <param name="localKey">Local key.</param>
		/// <param name="localName">Local name.</param>
		public void AddNamePair(string localKey, string localName)
		{
			this.namePairs.Add(localKey, localName);
		}

		/// <summary>
		/// Adds the description pair.
		/// </summary>
		/// <param name="localKey">Local key.</param>
		/// <param name="localName">Local name.</param>
		public void AddDescriptionPair(string localKey, string localDescription)
		{
			this.descriptionPairs.Add(localKey, localDescription);
		}
	}
}


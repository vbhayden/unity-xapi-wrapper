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
	public class Activity : Object
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
		public ActivityMetaData Definition
		{
			get
			{
				return this.definition;
			}
			set
			{
				this.definition = value;
			}
		}

		// Private fields
		private string id;
		private ActivityMetaData definition;

		/// <summary>
		/// Creates a new Activity instance from the given ID.
		/// </summary>
		/// <param name="id">Identifier IRI.</param>
		public Activity(string id = "http://activity.com/id", string name = null)
		{
			this.objectType = "Activity";
			this.id = id;

            // Assign the name if necessary
            if (name != null)
            {
                this.definition = new ActivityMetaData();
                this.definition.AddNamePair("en-US", name);
            }
		}
	}

	/// <summary>
	/// Activity meta data related to an Activity.
	/// 
	/// This object is an optional parameter on Activity-Type Objects.
	/// 
	/// !NOTE: "Extensions" have not yet not been implemented.
	/// </summary>
	[System.Serializable]
	public class ActivityMetaData
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
				return this.namePairs;
			}
			set
			{
				this.namePairs = value;
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
				return this.descriptionPairs;
			}
			set
			{
				this.descriptionPairs = value;
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
			set
			{
				this.activityType = value;
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
			set
			{
				this.moreInfo = value;
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
		public ActivityMetaData(string activityType = null, string moreInfo = null)
		{
			this.activityType = activityType;
			this.moreInfo = moreInfo;

			// We may not need these
			this.namePairs = new Dictionary<string, string>();
			this.descriptionPairs = new Dictionary<string, string>();
		}

        /// <summary>
        /// Check if the metadata instance has a given name key.
        /// </summary>
        /// <param name="localKey"></param>
        public bool HasNameKey(string localKey)
        {
            return this.namePairs.ContainsKey(localKey);
        }

        /// <summary>
        /// Check if the metadata instance has a description key.
        /// </summary>
        /// <param name="localKey"></param>
        public bool HasDescriptionKey(string localKey)
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


using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	[System.Serializable]
	public class Statement : XObject
	{
		/// <summary>
		/// Gets the main actor for this Statement.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("actor", NullValueHandling = NullValueHandling.Ignore)]
		public XActor MainActor
		{
			get { return this.mainActor; }
			set { this.mainActor = value; }
		}

		/// <summary>
		/// Gets the main verb for this Statement.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("verb", NullValueHandling = NullValueHandling.Ignore)]
		public XVerb MainVerb
		{
			get { return this.mainVerb; }
			set { this.mainVerb = value; }
		}

		/// <summary>
		/// Gets the main object for this Statement.
		/// 
		/// This can be another Actor, another Statement, or anything descending from XObject.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
		public XObject MainObject
		{
			get { return this.mainObject; }
			set { this.mainObject = value; }
		}

		/// <summary>
		/// Gets the main result for this Statement.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
		public XResult MainResult
		{
			get { return this.mainResult; }
			set { this.mainResult = value; } 
		}

		/// <summary>
		/// Gets the main verb for this Statement.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
		public XContext MainContext
		{
			get { return this.mainContext; }
			set { this.mainContext = value; } 
		}

		/// <summary>
		/// Gets the pseudo-random UUID for this statement.
		/// 
		/// This can be set manually if desired.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string UUID
		{
			get { return (this.isSubStatement) ? null : this.uuid; }
			set { this.uuid = value; }
		}

		/// <summary>
		/// Gets the TimeStamp attached to this statement.
		/// 
		/// To set a new timestamp, you can either refresh the timestamp using RefreshTimestamp() or creating 
		/// one manually using XAPIUtil's DateTime extension .ToISOString().
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
		public string Timestamp
		{
			get { return this.timestamp; }
			set { this.timestamp = value; }
		}

		/// <summary>
		/// Gets the timestamp date in DateTime format.
		/// </summary>
		/// <value>The timestamp date.</value>
		[JsonIgnore]
		public DateTime TimestampDate
		{
			get
			{ 
				return DateTime.Parse(this.Timestamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this statement is intended to be a sub statement of another statement.
		/// </summary>
		/// <value><c>true</c> if this instance is sub statement; otherwise, <c>false</c>.</value>
		[JsonIgnore]
		public bool IsSubStatement
		{
			get { return this.isSubStatement; }
			set
			{
				// If we intended for this to be a sub statement, then we need to change the object type here to reflect that
				//
				this.isSubStatement = value;
				this.objectType = (this.isSubStatement) ? "SubStatement" : null;
			}
		}

		/// <summary>
		/// Gets / Sets when this statement was stored.
		/// </summary>
		/// <value>The timestamp.</value>
		[JsonProperty("stored", NullValueHandling = NullValueHandling.Ignore)]
		public string Stored { get; set; }

		/// <summary>
		/// Gets the main actor for this Statement.
		/// </summary>
		/// <value>The main actor.</value>
		[JsonProperty("authority", NullValueHandling = NullValueHandling.Ignore)]
		public XActor Authority { get; set; }

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>The timestamp.</value>
		[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
		public string Version { get; set; }

		// Private Fields
		private XActor mainActor;
		private XVerb mainVerb;
		private XObject mainObject;
		private XResult mainResult;
		private XContext mainContext;
		private string timestamp;
		private string uuid;
		private bool isSubStatement = false;

		/// <summary>
		/// Explicit Constructor for an XAPI Statement.
		/// 
		/// Accepts an Actor, Verb, and Object.
		/// </summary>
		public Statement(XActor sActor, XVerb sVerb, XObject sObject)
		{
			// We need these 3 at a minimum to make a statement.
			this.mainActor = sActor;
			this.mainVerb = sVerb;
			this.mainObject = sObject;
            
			// Basic stuff
			this.timestamp = XAPIUtil.CurrentTimeISO;
			this.Version = XAPIUtil.XAPI_VERSION;

			// Assume we are not a substatement 
			this.isSubStatement = false;
			this.objectType = null;
		}

		/// <summary>
		/// Refreshs the timestamp.
		/// </summary>
		public void RefreshTimestamp()
		{
			this.timestamp = XAPIUtil.CurrentTimeISO;
		}

        [JsonIgnore]
        public string SimpleTriple
        {
            get
            {
                if (this.MainObject is XActivity)
                {
                    var activity = this.MainObject as XActivity;
                    var displayName = activity.Id;
                    if (activity.MetaData != null && activity.MetaData.HasNameKey("en-US"))
                        displayName = activity.MetaData.NamePairs["en-US"];

                    return string.Format("{0}, {1}, {2}", this.MainActor.Name, this.MainVerb.Name, displayName);
                }
                else
                {
                    return string.Format("{0}, {1}, {2}", this.MainActor.Name, this.MainVerb.Name, "unknown object");
                }
            }
        }
	}
}

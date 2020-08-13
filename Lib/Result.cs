using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// Class representing an XAPI Statement Result.
	/// 
	/// An optional property that represents a measured outcome related to the Statement in which it is included.
	/// </summary>
	[System.Serializable]
	public class Result
	{
		/// <summary>
		/// Gets or Sets whether the user was successful.
		/// </summary>
		/// <value>The scaled.</value>
		[JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
		public bool? Success
		{
			get { return this.success; }
			set { this.success = value; }
		}

		/// <summary>
		/// Gets or sets whether the user completed the task.
		/// </summary>
		/// <value>The minimum.</value>
		[JsonProperty("completion", NullValueHandling = NullValueHandling.Ignore)]
		public bool? Completion
		{
			get { return this.completion; }
			set { this.completion = value; }
		}

		/// <summary>
		/// Gets or sets the user's Response.
		/// </summary>
		/// <value>The max.</value>
		[JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
		public string Response
		{
			get { return this.response; }
			set { this.response = value; }
		}

		/// <summary>
		/// Gets or sets the user's Response.
		/// </summary>
		/// <value>The max.</value>
		[JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
		public string DurationString { get; set; }

		/// <summary>
		/// Gets or sets the duration.
		/// </summary>
		/// <value>The duration.</value>
		[JsonIgnore]
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
				this.ConvertDuration(value);
			}
		}

		/// <summary>
		/// Gets or sets the user's Response.
		/// </summary>
		/// <value>The max.</value>
		[JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
		public Score Score
		{
			get { return this.score; }
			set { this.score = value; }
		}

		/// <summary>
		/// Gets or sets the extensions.
		/// 
		/// IMPORTANT: Each Key provided MUST be an IRI.  You are allowed to pass anything with an IRI format
		/// and the value doesn't need to be an actual navigable value.
		/// 
		/// ALSO: The Value must be serializable.  If you pass a string or number, then this is guaranteed.  
		/// For custom values / objects, those objects / classes must be serializable.
		/// 
		/// Lastly, By the XAPI Spec, the LRS MUST accept ANY extension value.  So, as long as your key is valid, the value
		/// will be fine.
		/// 
		/// To prevent this dictionary from being seen in the JSON version of your Statement, set this to null.
		/// </summary>
		/// <value>The extensions.</value>
		[JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, object> Extensions
		{
			get
			{ 
				return this.extensions;
			}
			set
			{
				if (this.extensions != value)
					this.extensions = value;
			}
		}

		// Private fields
		private bool? success;
		private bool? completion;
		private string response;
		private TimeSpan duration;
		private Score score;
		private Dictionary<string, object> extensions;

		/// <summary>
		/// Constructs a StatementResult object from 
		/// 
		/// According to the Spec, a Result Object is not required to have  anything.
		/// </summary>
		[JsonConstructor]    
		public Result(bool? success = null, bool? completion = null)
		{
			this.success = success;
			this.completion = completion;
			this.extensions = new Dictionary<string, object>();
		}

		/// <summary>
		/// Converts the duration to an ISO 8601 duration string.
		/// </summary>
		/// <returns>The duration.</returns>
		/// <param name="span">Span.</param>
		private string ConvertDuration(TimeSpan span)
		{
			return System.Xml.XmlConvert.ToString(span);
		}
	}

	/// <summary>
	/// Score Object for an XAPI Statement's Result Object.
	/// </summary>
	[System.Serializable]
	public class Score
	{
		/// <summary>
		/// Gets or Sets the raw score.
		/// </summary>
		/// <value>The scaled.</value>
		[JsonProperty("raw", NullValueHandling = NullValueHandling.Ignore)]
		public float? Raw
		{
			get { return this.raw; }
			set { this.scaled = value; }
		}

		/// <summary>
		/// Gets or sets the minimum score possible.
		/// </summary>
		/// <value>The minimum.</value>
		[JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
		public float? Min
		{
			get { return this.min; }
			set { this.min = value; }
		}

		/// <summary>
		/// Gets or sets the maximum score possible.
		/// </summary>
		/// <value>The max.</value>
		[JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
		public float? Max
		{
			get { return this.max; }
			set { this.max = value; }
		}

		/// <summary>
		/// Gets / Sets the Scaled score.
		/// </summary>
		/// <value>The scaled.</value>
		[JsonProperty("scaled", NullValueHandling = NullValueHandling.Ignore)]
		public float? Scaled
		{
			get { return this.scaled; }
			set
			{
				if (value.HasValue)
					this.scaled = Mathf.Clamp(value.Value, -1f, 1f);
				else
					this.scaled = null;
			}
		}

		// Private fields, these will be nullable for json convenience
		private float? scaled;
		private float? raw;
		private float? min;
		private float? max;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.XScore"/> class.
		/// 
		/// All values are optional, the Spec does not require anything for a Score object.
		/// </summary>
		[JsonConstructor]
		public Score(float? raw = null, float? min = null, float? max = null)
		{
			this.raw = raw;
			this.min = min;
			this.max = max;

			// If we have all of these, we can assign a scaled value
			if (this.raw.HasValue && this.min.HasValue && this.max.HasValue && this.min != this.max)
			{
				// Simple conversion
				float rawScaled = (this.raw.Value - this.min.Value) / (this.max.Value - this.min.Value);

				// Spec states that these need to be between -1 and 1
				this.scaled = Mathf.Clamp(rawScaled, -1f, 1f);
			}
		}
	}
}


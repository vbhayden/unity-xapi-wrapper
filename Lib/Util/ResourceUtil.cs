using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XAPI
{
	/// <summary>
	/// Interface for all QueryArgument classes to be used during the call stage.
	/// </summary>
	public interface IQueryArguments
	{
		/// <summary>
		/// Converts the given set of arguments into a query string with format: ?key1=val1&key2=val2
		/// </summary>
		string ToQueryString();
	}

	/// <summary>
	/// XAPI resource.  Custom state resource objects should use this as a base class. 
	/// From the spec:
	/// 
	/// Generally, this is a scratch area for Learning Record Providers that do not have their own internal storage, or need to persist state across devices.
	/// This class is intended to support resources for States, Agent profiles, and Activity Profiles.
	/// </summary>
	[System.Serializable]
	public class FlexibleResource
	{
		// Settings
		public const long MAX_FILE_SIZE = 5000000;

		/// <summary>
		/// Key-Value mappings defined within this object.  The Values will only work with serializable
		/// classes / structs and primatives.
		/// </summary>
		[JsonProperty("objects", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, object> ObjectMap { get; set; }

		/// <summary>
		/// Key-File mappings defined within this object.  It's not recommended to manipulate this directly,
		/// instead use the helper functions AddFile and RemoveFile if you're uncertain about the serialization
		/// process for files.
		/// </summary>
		[JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> FileMap { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.FlexibleResource"/> class.
		/// </summary>
		public FlexibleResource()
		{
			this.ObjectMap = new Dictionary<string, object>();
			this.FileMap = new Dictionary<string, string>();
		}

		/// <summary>
		/// Adds the file at the FULLY QUALIFIED, ABSOLUTE PATH to this resource object.
		/// 
		/// The file path will be used to find the file, with the fileName 
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="fileName">File name.</param>
		public void AddFile(string fileKey, string filePath, bool limitFileSize = true)
		{
			// They'll be passing the path to the file they want to encode, so we'll need to do
			// the io stuff.
			//
			if (File.Exists(filePath) == false)
			{
				Debug.LogErrorFormat("FlexibleResource.AddFile:: File Not Found: {0}", filePath);
				return;
			}

			// If we're still here, it must exist
			FileInfo info = new FileInfo(filePath);

			// If this is within tolerance, we'll upload it
			if (info.Length >= FlexibleResource.MAX_FILE_SIZE && limitFileSize)
			{
				Debug.LogErrorFormat("FlexibleResource.AddFile:: File Size Exceeds Limit of {0} bytes.", FlexibleResource.MAX_FILE_SIZE);
				Debug.LogErrorFormat("FlexibleResource.AddFile:: Size: {0} bytes for {1}.", info.Length, filePath);
				Debug.LogErrorFormat("FlexibleResource.AddFile:: Bypass this message by adding optional argument 'limitFileSize: false'");
				return;
			}

			// So we'll need to read the bytes as is, then encode that into a string
			byte[] buffer = File.ReadAllBytes(filePath);
			string utf = System.Text.Encoding.UTF8.GetString(buffer);

			// Then save that to our map with whatever name the user chose
			this.FileMap.Add(fileKey, utf);

			// And clear our buffer
			buffer = null;
		}

		/// <summary>
		/// Removes the file.
		/// </summary>
		/// <param name="fileKey">File key.</param>
		public void RemoveFile(string fileKey)
		{
			// Clear it out, but don't proc the c# error for missing key
			if (this.FileMap.ContainsKey(fileKey))
			{
				this.FileMap.Remove(fileKey);
			}
			else
			{
				Debug.LogWarningFormat("FlexibleResource.RemoveFile:: No file found for key {0}.");
			}
		}

		/// <summary>
		/// Returns the file specified by a file key to its original byte format.
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="fileKey">File key.</param>
		private byte[] FileBytes(string fileKey)
		{
			// Check if we have this at all
			if (this.FileMap.ContainsKey(fileKey))
				return System.Text.Encoding.UTF8.GetBytes(this.FileMap[fileKey]);
			else
				return null;
		}

		/// <summary>
		/// Attempts to write the data with the given key to a file at the specified path.  The path
		/// should include the file extension.  (.docx, .xlsx., etc).
		/// 
		/// Will return <c>true</c> if successful and <c>false</c> if a problem occured, along with
		/// a message to the Unity console explaining the problem.
		/// </summary>
		/// <returns><c>true</c>, if to file was writed, <c>false</c> otherwise.</returns>
		/// <param name="fileKey">File key.</param>
		/// <param name="targetPath">Target path.</param>
		public bool WriteToFile(string fileKey, string targetPath, bool overwrite = false)
		{
			// Get the data for this key
			byte[] fileData = this.FileBytes(fileKey);

			// If this is null, we didn't have the key
			if (fileData == null)
			{
				Debug.LogWarningFormat("FlexibleResource.WriteToFile: File Key not found, cannot write to file.");
				return false;
			}

			// If we have data, we need to write it.  First, check if there's an existing file at this path
			if (File.Exists(targetPath) && overwrite == false)
			{
				Debug.LogWarningFormat("FlexibleResource.WriteToFile: File exists at path and overwrite permission not given, will not write to file."
					+ " To grant permission, use the optional argument:: overwrite: true");
				return false;
			}

			// We should be cleared to write the file now.   This bit was taken from:
			// https://stackoverflow.com/questions/6397235/write-bytes-to-file
			//
			try
			{
				using (var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
				{
					fs.Write(fileData, 0, fileData.Length);
					return true;
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarningFormat("FlexibleResource.WriteToFile: Cannot write file, Unexpected Exception was thrown: " + ex.Message);
				return false;
			}
		}
	}

	/// <summary>
	/// Query parameters for getting or setting State values with the LRS.
	/// </summary>
	public class StateResourceQueryArgs : IQueryArguments
	{
		// Names for spec compliance
		private const string SPEC_ACTIVITY_ID = "activityId";
		private const string SPEC_AGENT = "agent";
		private const string SPEC_REGISTRATION = "registration";
		private const string SPEC_STATE_ID = "stateId";
		private const string SPEC_SINCE = "since";

		/// <summary>
		/// Gets or sets the activity key.  The query will return all state values with this Activity ID.
		/// </summary>
		/// <value>The activity key.</value>
		public string ActivityKey{ get; set; }

		/// <summary>
		/// Gets or sets the actor.  The query will return all state values involving this Agent.
		/// 
		/// The Actor CANNOT be a Group.  This MUST be an Agent.
		/// </summary>
		/// <value>The actor.</value>
		public Actor Agent{ get; set; }

		/// <summary>
		/// Gets or sets the state key.  This property is used to assign a specific state key when POSTing an activity state
		/// to the LRS, but the ADL LRS also uses this query argument to limit which states are retrieved on GET requests.
		/// 
		/// If a StateKey is provided, the query will return a single document / instance.
		/// </summary>
		/// <value>The state key.</value>
		public string StateKey{ get; set; }

		/// <summary>
		/// Gets or sets the UUID used for registration.  This property is Optional.
		/// </summary>
		/// <value>The registration.</value>
		public string Registration{ get; set; }

		/// <summary>
		/// Gets or sets a timestamp used for narrowing the query.  Only ids of states stored since the specified Timestamp (exclusive) are returned.
		/// </summary>
		/// <value>The since.</value>
		public string Since { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.StateResourceQueryArgs"/> class.
		/// </summary>
		/// <param name="activityKey">Activity key.</param>
		/// <param name="agent">Agent.</param>
		/// <param name="registration">Registration.</param>
		/// <param name="stateKey">State key.</param>
		public StateResourceQueryArgs(string activityKey = "", Actor agent = null)
		{
			// We'll assume the key is correct
			this.ActivityKey = activityKey;

			// But the actor MUST be an Agent
			this.Agent = agent;
		}

		/// <summary>
		/// Converts this object into a query string with format: ?key1=val1&key2=val2&...
		/// </summary>
		/// <returns>The query string.</returns>
		public string ToQueryString()
		{
			// Add our values to a NameValueCollection
			NameValueCollection args = new NameValueCollection();

			// Add each field here
			if (this.Agent == null || this.ActivityKey == null || this.StateKey == null)
				Debug.LogErrorFormat("StateResourceQueryArgs.ToQueryString:: Each state query MUST have an Agent, ActivityKey, and StateKey.");

			// Keep going even though this not be a successful request if those aren't included
			if (this.ActivityKey != null)
				args.Add(StateResourceQueryArgs.SPEC_ACTIVITY_ID, this.ActivityKey);
			if (this.Registration != null)
				args.Add(StateResourceQueryArgs.SPEC_REGISTRATION, this.Registration);
			
			// We need to serialize the agent here
			if (this.Agent != null)
			{
				string agentString = JsonConvert.SerializeObject(this.Agent);
				args.Add(StateResourceQueryArgs.SPEC_AGENT, agentString);
			}

			// The last value will change depending on whether or not we're POSTing or GETing states
			//
			if (this.StateKey != null)
				args.Add(StateResourceQueryArgs.SPEC_STATE_ID, this.StateKey);
			if (this.Since != null)
				args.Add(StateResourceQueryArgs.SPEC_SINCE, this.Since);

			// Once we have our arguments, use the extension
			return args.ToQueryString();
		}
	}

	/// <summary>
	/// The Agents Resource provides a method to retrieve a special Object with combined information about 
	/// an Agent derived from an outside service, such as a directory service. 
	/// This resource has Concurrency controls associated with it.
	/// </summary>
	[System.Serializable]
	public class AgentResource : Object
	{
		/// <summary>
		/// Gets or sets the list of names belonging to this Agent.
		/// </summary>
		/// <value>The agent.</value>
		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string[] AgentNames{ get; set; }

		/// <summary>
		/// Gets or sets the list of mailboxes belonging to this Agent.
		/// </summary>
		/// <value>The agent mailboxes.</value>
		[JsonProperty("mbox", NullValueHandling = NullValueHandling.Ignore)]
		public string[] AgentMailboxes{ get; set; }

		/// <summary>
		/// Gets or sets the list of mailboxes belonging to this Agent.
		/// </summary>
		/// <value>The agent mailboxes.</value>
		[JsonProperty("mbox_sha1sum", NullValueHandling = NullValueHandling.Ignore)]
		public string[] AgentSHA1Mailboxes{ get; set; }

		/// <summary>
		/// Gets or sets the list of mailboxes belonging to this Agent.
		/// </summary>
		/// <value>The agent mailboxes.</value>
		[JsonProperty("openid", NullValueHandling = NullValueHandling.Ignore)]
		public string[] AgentOpenIDs{ get; set; }

		/// <summary>
		/// Gets or sets the list of mailboxes belonging to this Agent.
		/// </summary>
		/// <value>The agent mailboxes.</value>
		[JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
		public string[] AgentAccounts{ get; set; }

		/// <summary>
		/// Return a special, Person Object for a specified Agent. The Person Object is very similar 
		/// to an Agent Object, but instead of each attribute having a single value, each attribute 
		/// has an array value, and it is legal to include multiple identifying properties. This is 
		/// different from the FOAF concept of person, person is being used here to indicate a 
		/// person-centric view of the LRS Agent data, but Agents just refer to one persona (a person in one context).
		/// </summary>
		public AgentResource()
		{
			this.objectType = "Person";
		}
	}

	/// <summary>
	/// Activity resource query arguments.
	/// 
	/// Similar to Agent queries, Activity queries don't really need their own class, but will
	/// get one for uniformity's sake.
	/// </summary>
	public class AgentResourceQueryArgs : IQueryArguments
	{
		// Parameter
		private const string SPEC_AGENT = "agent";

		/// <summary>
		/// Gets or sets the activity key mapping to a specific Activity in the LRS.
		/// </summary>
		/// <value>The activity key.</value>
		public Actor Agent { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.ActivityResourceQueryArgs"/> class.
		/// </summary>
		public AgentResourceQueryArgs(Actor agent)
		{
			this.Agent = agent;
		}

		/// <summary>
		/// Converts this object into a query string with format: ?key1=val1&key2=val2&...
		/// </summary>
		/// <returns>The query string.</returns>
		public string ToQueryString()
		{
			// Add our values to a NameValueCollection
			NameValueCollection args = new NameValueCollection();

			// Add each field here
			if (this.Agent == null)
				Debug.LogErrorFormat("ActivityResourceQueryArgs.ToQueryString:: Each agent query MUST have an Agent.");

			// There's only one thing here to use
			if (this.Agent != null)
			{
				string agentString = JsonConvert.SerializeObject(this.Agent);
				args.Add(AgentResourceQueryArgs.SPEC_AGENT, agentString);
			}

			// Once we have our arguments, use the extension
			return args.ToQueryString();
		}
	}

	/// <summary>
	/// Agent resource query arguments.
	/// 
	/// Agent queries are a bit simpler in that we only need a single Agent as the parameter.
	/// Since the other queries have more arguments and warrant their own classes, this will 
	/// still be its own class for uniformity, although it is absolutely not necessary for functionality.
	/// </summary>
	public class AgentProfileQueryArgs : IQueryArguments
	{
		// Parameter names
		private const string SPEC_AGENT = "agent";
		private const string SPEC_PROFILE_KEY = "profileId";
		private const string SPEC_SINCE = "since";

		/// <summary>
		/// Gets or sets the agent that we're querying about.
		/// 
		/// This is an Actor instance and MUST be an Agent type.
		/// </summary>
		/// <value>The agent.</value>
		public Actor Agent { get; set; }

		/// <summary>
		/// Gets or sets the profile key that we're assigning to the LRS.
		/// 
		/// If a ProfileKey is assigned when getting Agent Profiles, only the profile whose ID
		/// matches the given ProfileKey will be returned.
		/// 
		/// A profile key is REQUIRED when submitting profile data to the LRS.
		/// </summary>
		/// <value>The profile key.</value>
		public string ProfileKey { get; set; }

		/// <summary>
		/// Gets or sets a timestamp for the query.  Any agent profiles made prior to this timestamp
		/// will be ignored on the query.
		/// </summary>
		/// <value>The since.</value>
		public string Since { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.AgentResourceQueryArgs"/> class.
		/// </summary>
		/// <param name="agent">Agent.</param>
		public AgentProfileQueryArgs(Actor agent, string profileKey)
		{
			this.Agent = agent;
			this.ProfileKey = profileKey;
		}

		/// <summary>
		/// Converts this object into a query string with format: ?key1=val1&key2=val2&...
		/// </summary>
		/// <returns>The query string.</returns>
		public string ToQueryString()
		{
			// Add our values to a NameValueCollection
			NameValueCollection args = new NameValueCollection();

			// Add each field here
			if (this.Agent == null || this.ProfileKey == null)
				Debug.LogErrorFormat("AgentProfileQueryArgs.ToQueryString:: Each agent profile query MUST have an Agent and Profile Key.");

			// We need to serialize the agent here
			if (this.Agent != null)
			{
				string agentString = JsonConvert.SerializeObject(this.Agent);
				args.Add(AgentProfileQueryArgs.SPEC_AGENT, agentString);
			}

			// The last value will change depending on whether or not we're POSTing or GETing states
			//
			if (this.ProfileKey != null)
				args.Add(AgentProfileQueryArgs.SPEC_PROFILE_KEY, this.ProfileKey);
			if (this.Since != null)
				args.Add(AgentProfileQueryArgs.SPEC_SINCE, this.Since);

			// Once we have our arguments, use the extension
			return args.ToQueryString();
		}
	}

	//
	// Activity Objects are already defined as Activity.  These objects will be returned from
	// the LRS with each Activity query in that same format, so we do not need to define them here.
	//

	/// <summary>
	/// Activity resource query arguments.
	/// 
	/// Similar to Agent queries, Activity queries don't really need their own class, but will
	/// get one for uniformity's sake.
	/// </summary>
	public class ActivityResourceQueryArgs : IQueryArguments
	{
		// Parameter
		private const string SPEC_ACTIVITY_KEY = "activityId";

		/// <summary>
		/// Gets or sets the activity key mapping to a specific Activity in the LRS.
		/// </summary>
		/// <value>The activity key.</value>
		public string ActivityKey { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.ActivityResourceQueryArgs"/> class.
		/// </summary>
		public ActivityResourceQueryArgs(string activityKey)
		{
			this.ActivityKey = activityKey;
		}

		/// <summary>
		/// Converts this object into a query string with format: ?key1=val1&key2=val2&...
		/// </summary>
		/// <returns>The query string.</returns>
		public string ToQueryString()
		{
			// Add our values to a NameValueCollection
			NameValueCollection args = new NameValueCollection();

			// Add each field here
			if (this.ActivityKey == null)
				Debug.LogErrorFormat("ActivityResourceQueryArgs.ToQueryString:: Each state query MUST have an Activity Key.");

			// There's only one thing here to use
			if (this.ActivityKey != null)
				args.Add(ActivityResourceQueryArgs.SPEC_ACTIVITY_KEY, this.ActivityKey);

			// Once we have our arguments, use the extension
			return args.ToQueryString();
		}
	}

	/// <summary>
	/// Activity profile resource query arguments.
	/// </summary>
	public class ActivityProfileQueryArgs : IQueryArguments
	{
		// Parameter names
		private const string SPEC_ACTIVITY_KEY = "activityId";
		private const string SPEC_PROFILE_KEY = "profileId";
		private const string SPEC_SINCE = "since";

		/// <summary>
		/// Gets or sets the agent that we're querying about.
		/// 
		/// This is an Actor instance and MUST be an Agent type.
		/// </summary>
		/// <value>The agent.</value>
		public string ActivityKey { get; set; }

		/// <summary>
		/// Gets or sets the profile key that we're assigning to the LRS. 
		/// 
		/// If a ProfileKey is given when getting Activity Profiles, only the profile whose ID
		/// matches the given ProfileKey will be returned.
		/// 
		/// A profile key is REQUIRED when submitting profile data to the LRS.
		/// </summary>
		/// <value>The profile key.</value>
		public string ProfileKey { get; set; }

		/// <summary>
		/// Gets or sets a timestamp for the query.  Any agent profiles made prior to this timestamp
		/// will be ignored on the query.
		/// </summary>
		/// <value>The since.</value>
		public string Since { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.AgentResourceQueryArgs"/> class.
		/// </summary>
		/// <param name="agent">Agent.</param>
		public ActivityProfileQueryArgs(string activityKey, string profileKey)
		{
			this.ActivityKey = activityKey;
			this.ProfileKey = profileKey;
		}

		/// <summary>
		/// Converts this object into a query string with format: ?key1=val1&key2=val2&...
		/// </summary>
		/// <returns>The query string.</returns>
		public string ToQueryString()
		{
			// Add our values to a NameValueCollection
			NameValueCollection args = new NameValueCollection();

			// Add each field here
			if (this.ActivityKey == null || this.ProfileKey == null)
				Debug.LogErrorFormat("ActivityProfileQueryArgs.ToQueryString:: Each agent profile query MUST have an Activity Key and Profile Key.");

			// We need to serialize the agent here
			if (this.ActivityKey != null)
				args.Add(ActivityProfileQueryArgs.SPEC_ACTIVITY_KEY, this.ActivityKey);

			// These are optional when pulling information, but we need ProfileKey when pushing.
			//
			if (this.ProfileKey != null)
				args.Add(ActivityProfileQueryArgs.SPEC_PROFILE_KEY, this.ProfileKey);
			if (this.Since != null)
				args.Add(ActivityProfileQueryArgs.SPEC_SINCE, this.Since);

			// Once we have our arguments, use the extension
			return args.ToQueryString();
		}
	}

	/// <summary>
	/// Small object containing information about the LRS in use.
	/// </summary>
	[System.Serializable]
	public class AboutResource
	{
		/// <summary>
		/// Gets or sets the list of names belonging to this Agent.
		/// </summary>
		/// <value>The agent.</value>
		[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
		public string[] XAPIVersions { get; set; }

		/// <summary>
		/// Raw string JSON object of other properties returned by the LRS.  For an attempted parse of
		/// this JSON object, use ExtensionMap.
		/// </summary>
		/// <value>The agent mailboxes.</value>
		[JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, object> Extensions { get; set; }

		/// <summary>
		/// An attempted parsing for the extensions map.  This is potentially nested set of <string, object>
		/// dictionaries.
		/// </summary>
		/// <value>The extension map.</value>
		[JsonIgnore]
		public Dictionary<string, object> ExtensionMap { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.AboutResource"/> class.
		/// 
		/// DO NOT USE MANUALLY.
		/// </summary>
		public AboutResource()
		{
			// Just for JSON, do not use.
		}
	}
}




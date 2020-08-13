using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// Class representing the optional Context component of an XAPI Statement.
	/// </summary>
	[System.Serializable]
	public class Context
	{
		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("registration", NullValueHandling = NullValueHandling.Ignore)]
		public string Registration
		{
			get { return this.registration; }
			set { this.registration = value; }
		}

		/// <summary>
		/// Instructor that the Statement relates to, if not included as the Actor of the Statement.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("instructor", NullValueHandling = NullValueHandling.Ignore)]
		public Actor Instructor
		{
			get { return this.instructor; }
			set { this.instructor = value; }
		}

		/// <summary>
		/// Team that this Statement relates to, if not included as the Actor of the Statement.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
		public Actor Team
		{
			get { return this.team; }
			set { this.team = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("contextActivities", NullValueHandling = NullValueHandling.Ignore)]
		public ContextActivities ContextActivities
		{
			get { return this.contextActivities; }
			set { this.contextActivities = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("revision", NullValueHandling = NullValueHandling.Ignore)]
		public string Revision
		{
			get { return this.revision; }
			set { this.revision = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
		public string Platform
		{
			get { return this.platform; }
			set { this.platform = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
		public string Language
		{
			get { return this.language; }
			set { this.language = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("statement", NullValueHandling = NullValueHandling.Ignore)]
		public StatementRef Statement
		{
			get { return this.statement; }
			set { this.statement = value; }
		}

		/// <summary>
		/// The registration that the Statement is associated with.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary <string, string> Extensions
		{
			get { return this.extensions; }
			set { this.extensions = value; }
		}


		// Private fields
		private string registration;
		private Actor instructor;
		private Actor team;
		private ContextActivities contextActivities;
		private string revision;
		private string platform;
		private string language;
		private StatementRef statement;
		private Dictionary<string, string> extensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.Context"/> class.
		/// 
		/// All serializable member values are exposed, but must be set manually at the current time.
		/// </summary>
		[JsonConstructor]    
		public Context()
		{
			
		}
	}

	[System.Serializable]
	public class ContextActivities
	{
		/// <summary>
		/// List of Activity IRLs with a direct relation to the Activity which is the Object of the Statement. 
		/// In almost all cases there is only one sensible parent or none, not multiple. 
		/// For example: a Statement about a quiz question would have the quiz as its parent Activity.
		/// </summary>
		/// <value>The parent.</value>
		[JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
		public List<Activity> Parents
		{
			get { return this.parent; }
			set { this.parent = value; }
		}

		/// <summary>
		/// List of Activity IRLs with an indirect relation to the Activity which is the Object of the Statement. 
		/// For example: a course that is part of a qualification. The course has several classes. 
		/// The course relates to a class as the parent, the qualification relates to the class as the grouping.
		/// </summary>
		/// <value>The grouping.</value>
		[JsonProperty("grouping", NullValueHandling = NullValueHandling.Ignore)]
		public List<Activity> Groupings
		{
			get { return this.grouping; }
			set { this.grouping = value; }
		}

		/// <summary>
		/// List of Activity IRLs used to categorize the Statement. "Tags" would be a synonym. Category SHOULD be used to indicate a profile 
		/// of xAPI behaviors, as well as other categorizations. For example: Anna attempts a biology exam, and the 
		/// Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the 
		/// category is the cmi5 profile.
		/// </summary>
		/// <value>The category.</value>
		[JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
		public List<Activity> Categories
		{
			get { return this.category; }
			set { this.category = value; }
		}

		/// <summary>
		/// List of Activity IRLs for a contextActivity that don't fit one of the other properties. 
		/// For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, 
		/// and the exam is a contextActivity of type other.
		/// </summary>
		/// <value>The other.</value>
		[JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
		public List<Activity> Other
		{
			get { return this.other; }
			set { this.other = value; }
		}

		// Private fields
		private List<Activity> parent;
		private List<Activity> grouping;
		private List<Activity> category;
		private List<Activity> other;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.ContextActivities"/> class.
		/// 
		/// Each List can be manipulated manually, or you can use the helper functions for basic
		/// functionality with the lists.
		/// </summary>
		[JsonConstructor]    
		public ContextActivities()
		{
			
		}

		/// <summary>
		/// Adds a parent activity (string instance as activity key).
		/// </summary>
		public void AddParent(string id)
		{
			this.AddToList(id, ref this.parent);
		}

		/// <summary>
		/// Adds a parent activity (Activity instance).
		/// </summary>
		/// <param name="parentActivity">Parent activity.</param>
		public void AddParent(Activity activity)
		{
			this.AddToList(activity, ref this.parent);
		}

		/// <summary>
		/// Clears parent-related activities from this instance.
		/// </summary>
		public void ClearParents()
		{
			this.ClearList(ref this.parent);
		}

		/// <summary>
		/// Adds a Grouping activity (string instance as activity key).
		/// </summary>
		public void AddGrouping(string id)
		{
			this.AddToList(id, ref this.grouping);
		}

		/// <summary>
		/// Adds a Grouping activity (Activity instance).
		/// </summary>
		/// <param name="GroupingActivity">Grouping activity.</param>
		public void AddGrouping(Activity activity)
		{
			this.AddToList(activity, ref this.grouping);
		}

		/// <summary>
		/// Clears Grouping-related activities from this instance.
		/// </summary>
		public void ClearGroupings()
		{
			this.ClearList(ref this.grouping);
		}

		/// <summary>
		/// Adds a category for this activity (string instance as activity key).
		/// </summary>
		public void AddCategory(string id)
		{
			this.AddToList(id, ref this.parent);
		}

		/// <summary>
		/// Adds a category for this activity (Activity instance).
		/// </summary>
		/// <param name="parentActivity">Parent activity.</param>
		public void AddCategory(Activity activity)
		{
			this.AddToList(activity, ref this.parent);
		}

		/// <summary>
		/// Clears parent-related activities from this instance.
		/// </summary>
		public void ClearCategories()
		{
			this.ClearList(ref this.category);
		}

		/// <summary>
		/// Adds a miscellaneous activity (string instance as activity key).
		/// </summary>
		public void AddOther(string id)
		{
			this.AddToList(id, ref this.other);
		}

		/// <summary>
		/// Adds a miscellaneous activity (Activity instance).
		/// </summary>
		/// <param name="parentActivity">Parent activity.</param>
		public void AddOther(Activity activity)
		{
			this.AddToList(activity, ref this.other);
		}

		/// <summary>
		/// Clears parent-related activities from this instance.
		/// </summary>
		public void ClearOther()
		{
			this.ClearList(ref this.other);
		}

		/// <summary>
		/// Internal helper to make this a bit more read-able.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="activityList">Activity list.</param>
		private void AddToList(Activity activity, ref List<Activity> activityList)
		{
			if (activityList == null)
				activityList = new List<Activity>();

			activityList.Add(activity);
		}

		/// <summary>
		/// Internal helper to make this a bit more read-able.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="activityList">Activity list.</param>
		private void AddToList(string activityKey, ref List<Activity> activityList)
		{
			if (activityList == null)
				activityList = new List<Activity>();

			this.AddToList(new Activity(activityKey), ref activityList);
		}

		/// <summary>
		/// Clears the list.
		/// </summary>
		/// <param name="activityList">Activity list.</param>
		private void ClearList(ref List<Activity> activityList)
		{
			if (activityList != null)
				activityList.Clear();

			activityList = null;
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace XAPI
{
	/// <summary>
	/// Type of actor being supplied to an XAPI Statement.
	/// </summary>
	public enum ActorType
	{
		Agent = 1,
		Group = 2,
	}

	/// <summary>
	/// Inverse Functional Identifier Types.
	/// 
	/// Only one is allowable by the XAPI Spec.
	/// </summary>
	public enum IFIType
	{
		Mailbox = 1,
		MailboxSHA1 = 2,
		OpenID = 3,
		Account = 4
	}

	/// <summary>
	/// Statement actor.
	/// 
	/// This class is intended to serve as the "Actor" for an XAPI statement, but may also serve as its "Object".
	/// </summary>
	[System.Serializable]
	public class Actor : Object
	{
		/// <summary>
		/// The given IFI used to identify this actor.
		/// </summary>
		[JsonIgnore]
		public IFIType ifi;

		/// <summary>
		/// The type of the actor.
		/// </summary>
		[JsonIgnore]
		public ActorType actorType;

		/// <summary>
		/// Mailbox ID assigned to this Actor.  Will return null if the actor's IFI isn't set to IFITypes.Mailbox.
		/// </summary>
		/// <value>The mailbox.</value>
		[JsonProperty("mbox", NullValueHandling = NullValueHandling.Ignore)]
		public string Mailbox
		{
			get
			{
				if (this.ifi != IFIType.Mailbox)
					return null;
				
				return (this.mailbox == null) ? null : "mailto:" + this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		/// <summary>
		/// SHA1 Encrypted actor mailbox.  Will return null if the actor's IFI isn't set to IFITypes.MailboxSHA1.
		/// </summary>
		/// <value>The mailbox SHA1.</value>
		[JsonProperty("mbox_sha1sum", NullValueHandling = NullValueHandling.Ignore)]
		public string MailboxSHA1
		{
			get
			{
				if (this.ifi != IFIType.MailboxSHA1)
					return null;
				
				return (this.mailbox == null) ? null : "NOT IMPLEMENTED";
			}
			set
			{
				this.mailboxSHA1 = value;
			}
		}

		/// <summary>
		/// Gets the OpenID for this actor.  Will return null if the actor's IFI is not IFITypes.OpenID.
		/// </summary>
		/// <value>The open I.</value>
		[JsonProperty("openid", NullValueHandling = NullValueHandling.Ignore)]
		public string OpenID
		{
			get
			{
				if (this.ifi != IFIType.OpenID)
					return null;

				return this.openID;
			}
			set
			{
				this.openID = value;
			}
		}

		/// <summary>
		/// Gets the OpenID for this actor.  Will return null if the actor's IFI is not IFITypes.OpenID.
		/// </summary>
		/// <value>The open I.</value>
		[JsonProperty("account", NullValueHandling = NullValueHandling.Ignore)]
		public ActorAccount Account
		{
			get
			{
				if (this.ifi != IFIType.Account)
					return null;

				return this.account;
			}
			set
			{
				this.account = value;
			}
		}

		/// <summary>
		/// Gets the type of the actor.
		/// 
		/// Overrides the StatementObject Property by the same name.
		/// </summary>
		/// <value>The type of the main actor.</value>
		[JsonProperty("objectType", NullValueHandling = NullValueHandling.Ignore)]
		public override string ObjectType
		{
			get
			{
				return (this.actorType == ActorType.Agent) ? "Agent" : "Group";
			}
		}

		/// <summary>
		/// Gets the Name of the actor.
		/// </summary>
		/// <value>The type of the main actor.</value>
		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name
		{
			get
			{
				return (this.name == "") ? null : this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>
		/// Gets the group members assigned to this Actor.
		/// </summary>
		/// <value>The group members.</value>
		[JsonProperty("member", NullValueHandling = NullValueHandling.Ignore)]
		public List<Actor> GroupMembers
		{
			get
			{
				// Make sure the JSON ignores this if we're not even a group
				if (this.actorType != ActorType.Group)
					return null;

				// If we ARE a group, then ostensibly we have members
				return this.groupMembers;
			}
		}

		/// <summary>
		/// Gets a guaranteed name from whatever the IFI format was with this Actor.
		/// </summary>
		/// <value>The name according to whatever .</value>
		[JsonIgnore]
		public string SimpleName
		{
			get
			{
				switch (this.ifi)
				{
					case IFIType.Account:
						return this.account.name;

					default:
					case IFIType.Mailbox:
						return this.name;

					case IFIType.MailboxSHA1:
						return this.mailboxSHA1;

					case IFIType.OpenID:
						return this.openID;
				}
			}
		}

		// Private properties for our IFI
		private string mailbox;
		private string mailboxSHA1;
		private string openID;
		private ActorAccount account;
		private List<Actor> groupMembers;
		private string name;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.Actor"/> class.
		/// 
		/// This is intended only for JSON,  do not use manually.
		/// </summary>
		[JsonConstructor]
		public Actor()
		{
			this.groupMembers = new List<Actor>();
		}

		/// <summary>
		/// Internal constructor only, do not use.
		/// </summary>
		/// <param name="ifi">Ifi.</param>
		private Actor(IFIType ifi, bool isGroup = false, string name = "")
		{
			// Assign our actor / ID types
			this.ifi = ifi;
			this.actorType = (isGroup) ? ActorType.Group : ActorType.Agent;
			this.name = name;

			// Start our members list.  We'll handle this at the JSON level if it ends up
			// not being necessary
			this.groupMembers = new List<Actor>();
		}

		/// <summary>
		/// Adds the group member.
		/// 
		/// Performing this action will change this Actor to a Group Type.
		/// </summary>
		/// <param name="groupMember">Group member.</param>
		public void AddGroupMember(Actor groupMember)
		{
			this.groupMembers.Add(groupMember);
			this.actorType = ActorType.Group;
		}

		/// <summary>
		/// Clears the group members.
		/// 
		/// Performing this action will remove all group members and revert this to an Agent type Actor.
		/// </summary>
		public void ClearGroupMembers()
		{
			this.groupMembers.Clear();
			this.actorType = ActorType.Agent;
		}

		/// <summary>
		/// Returns an Actor instance whose IFI is an unencrypted email address.
		/// </summary>
		/// <returns>The mailbox.</returns>
		/// <param name="mailbox">Mailbox.</param>
		public static Actor FromMailbox(string mailbox, bool isGroup = false, string name = "")
		{
			// Mailbox-style IFI
			Actor actor = new Actor(IFIType.Mailbox, isGroup, name);

			// Then set our mbox 
			actor.Mailbox = mailbox;

			return actor;
		}

		/// <summary>
		/// Returns an Actor instance whose IFI is an encrypted email address.
		/// </summary>
		/// <returns>The mailbox.</returns>
		/// <param name="mailbox">Mailbox.</param>
		public static Actor FromMailboxSHA1(string mailbox, bool isGroup = false, string name = "")
		{
			// Mailbox-style IFI
			Actor actor = new Actor(IFIType.MailboxSHA1, isGroup, name);

			// Then set our mbox 
			actor.Mailbox = mailbox;

			return actor;
		}

		/// <summary>
		/// Returns an Actor instance whose IFI is an OpenID URI.
		/// </summary>
		/// <returns>The mailbox.</returns>
		/// <param name="mailbox">Mailbox.</param>
		public static Actor FromOpenID(string id, bool isGroup = false, string name = "")
		{
			// Mailbox-style IFI
			Actor actor = new Actor(IFIType.OpenID, isGroup, name);

			// Then set our mbox 
			actor.OpenID = id;

			return actor;
		}

		/// <summary>
		/// Returns an Actor instance whose IFI is an OpenID URI.
		/// </summary>
		/// <returns>The mailbox.</returns>
		/// <param name="mailbox">Mailbox.</param>
		public static Actor FromAccount(ActorAccount actorAccount, bool isGroup = false, string name = "")
		{
			// Mailbox-style IFI
			Actor actor = new Actor(IFIType.Account, isGroup, name);

			// Then set our mbox 
			actor.Account = actorAccount;

			return actor;
		}

		/// <summary>
		/// Override allowing the user to enter the HomePage and Name fields of an ActorAccount
		/// object explicitly.
		/// </summary>
		/// <returns>The account.</returns>
		/// <param name="actorAccount">Actor account.</param>
		/// <param name="isGroup">If set to <c>true</c> is group.</param>
		public static Actor FromAccount(string homePage, string accountName, bool isGroup = false, string name = "")
		{
			// Mailbox-style IFI
			Actor actor = new Actor(IFIType.Account, isGroup, name);

			// Then set our mbox 
			actor.Account = new ActorAccount(homePage, accountName);

			return actor;
		}

        /// <summary>
        /// Estimate the IFI type of this actor.
        /// 
        /// As the serialization is done by ignoring properties irrelevant to certain IFI types,
        /// the IFI must be inferred once we receive a statement from an LRS.
        /// </summary>
        /// <returns></returns>
        public IFIType GuessIFI()
        {
            if (this.account != null)
                return IFIType.Account;
            else if (this.mailboxSHA1 != null)
                return IFIType.MailboxSHA1;
            else if (this.mailbox != null)
                return IFIType.Mailbox;
            else
                return IFIType.OpenID;
        }
	}

	/// <summary>
	/// Actor account.
	/// 
	/// As the only two values here are required strings, we can have a much simpler implementation.
	/// </summary>
	[System.Serializable]
	public class ActorAccount
	{
		/// <summary>
		/// The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.
		/// </summary>
		public string homePage = "";
		/// <summary>
		/// The unique id or name used to log in to this account. This is based on FOAF's accountName.
		/// </summary>
		public string name = "";

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.ActorAccount"/> class.
		/// 
		/// Both values must be supplied on construction.
		/// </summary>
		/// <param name="homePage">Home page.</param>
		/// <param name="name">Name.</param>
		public ActorAccount(string homePage, string name)
		{
			this.homePage = homePage;
			this.name = name;
		}
	}
}



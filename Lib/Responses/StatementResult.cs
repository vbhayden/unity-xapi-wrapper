using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;
using UnityEngine;

namespace XAPI
{
	/// <summary>
	/// Serializable class for the StatementResult Object outlined in the XAPI Spec.
	/// 
	/// This *IS NOT* the Result component of an XAPI Statement.  This is the result of
	/// a query to the LRS to retrieve XAPI Statements.
	/// </summary>
	[System.Serializable]
	public class StatementResult
	{
		/// <summary>
		/// Statements returned by the LRS Query.
		/// </summary>
		/// <value>The statements.</value>
		[JsonProperty("statements", NullValueHandling = NullValueHandling.Include)]
		public List<Statement> Statements
		{
			get { return this.statements; }
			set
			{
				if (this.statements != value && value != null)
				{
					this.statements = value;
				}	
			}
		}

		/// <summary>
		/// Gets the IRL usable to retrieve additional statements.
		/// </summary>
		/// <value>The statements.</value>
		[JsonProperty("more", NullValueHandling = NullValueHandling.Include)]
		public string MoreIRL
		{
			get { return this.moreIRL; }
			set { this.moreIRL = value; }
		}

		/// <summary>
		/// Gets the total number of statements returned.
		/// </summary>
		/// <value>The statements.</value>
		[JsonIgnore]
		public int StatementCount
		{
			get
			{
				return (this.statements == null) ? 0 : this.statements.Count;
			}
		}

		/// <summary>
		/// Gets whether or not there are more statements to retrieve.
		/// </summary>
		/// <value>The statement count.</value>
		[JsonIgnore]
		public bool MoreStatements
		{
			get { return !(this.moreIRL == null || this.moreIRL == ""); }
		}

		// Private fields
		private List<Statement> statements;
		private string moreIRL;

		/// <summary>
		/// Initializes a new instance of the <see cref="XAPI.StatementResult"/> class.
		/// </summary>
		public StatementResult()
		{
			this.statements = new List<Statement>();
			this.moreIRL = null;
		}

		/// <summary>
		/// Merge the specified original and recent StatementResult instances.
		/// 
		/// This will merge the Statement arrays and maintain the most recent IRL for the More property.
		/// </summary>
		/// <param name="original">Original.</param>
		/// <param name="recent">Recent.</param>
		public static StatementResult Merge(StatementResult original, StatementResult recent, int? limit = null)
		{
			// Create the result to return
			StatementResult merged = new StatementResult();

			// Check how many statements we need
			merged.Statements = new List<Statement>(original.StatementCount + recent.StatementCount);

			// Copy them into a new statement array
			merged.Statements.AddRange(original.Statements);
			merged.Statements.AddRange(recent.Statements);

			// Remove if necessary
			if (limit.HasValue && merged.StatementCount > limit.Value)
				merged.Statements.RemoveRange(limit.Value - 1, 1 + merged.StatementCount - limit.Value);

			// Then take the  most recent IRI
			merged.MoreIRL = recent.MoreIRL;

			// And return whatever we build
			return merged;
		}
	}
}


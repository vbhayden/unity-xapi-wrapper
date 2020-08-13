using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace XAPI
{
    /// <summary>
    /// Helper class to help when querying records from an LRS.
    /// </summary>
	public class StatementQuery 
	{  
        /// <summary>
        /// Default number of statements to include in an xAPI query.
        /// </summary>
        public const int DEFAULT_LIMIT = 50;

        /// <summary>
        /// Search for a specific verb using the Verb object.
        /// 
        /// This is a property that assigns the given verb ID as the query target.
        /// </summary>
        public Verb PredefinedVerb
        {
            set 
            {
                this.verbId = value.Id;
            }
        }

        /// <summary>
        /// Specific Verb ID to query.
        /// </summary>
        public string verbId;

        /// <summary>
        /// Specific Activity ID to query.
        /// 
        /// Activity IDs are intended to be unique within an LRS, but that cannot be enforced
        /// as two statements may use the same activity ID unknowingly.  This query will likely
        /// return several statements. 
        /// </summary>
        public string activityId;

        /// <summary>
        /// Specific Registration ID to query. 
        /// 
        /// Registration IDs are options components of an xAPI statement that deal primarily with
        /// information relevant to an LMS.  Not all statements will contain a registration ID, so
        /// be sure that the statements you intend to query do have this property.
        /// </summary>
        public string registrationId;

        /// <summary>
        /// Specific Statement ID to query.
        /// 
        /// This query should return only a single statement.  When you send a statement using this wrapper,
        /// one of the callback arguments is the statement ID assigned by the LRS.  If you store that locally
        /// (or within a PlayerPrefs value), then you can use this query argument to retrieve it.
        /// 
        /// This is particularly useful for keeping game state data in an LRS.
        /// </summary>
        public string statementId;

        /// <summary>
        /// Specific Voided Statement ID to query.
        /// 
        /// This query will retrieve a specific voided statement from the LRS.  
        /// </summary>
        public string voidedStatementId;
        
        /// <summary>
        /// Determines whether or not to included related agents in your query.
        /// 
        /// This is a nullable field and will not be included by default.  For more information on related agents,
        /// consult the xAPI Specification.
        /// </summary>
        public bool? relatedAgents;
        
        /// <summary>
        /// Determines whether or not to included related activities in your query.
        /// 
        /// This is a nullable field and will not be included by default.  For more information on related activities,
        /// consult the xAPI Specification.
        /// </summary>
        public bool? relatedActivities;

        /// <summary>
        /// Assign a limit to your query results.  By default, only 25 statements will be returned.
        /// </summary>
        public int? limit;

        /// <summary>
        /// Earliest statement timestamps to consider in your query.  
        /// 
        /// This does not filter the `stored` property, only the `timestamp`.
        /// </summary>
        public DateTime? sinceDate;
        
        /// <summary>
        /// Oldest statement timestamps to consider in your query.  
        /// 
        /// This does not filter the `stored` property, only the `timestamp`.
        /// </summary>
        public DateTime? untilDate;

        /// <summary>
        /// Default constructor.  Use this when you intend to populate your query
        /// manually.  By default, this query will not contain any query arguments.
        /// </summary>
        public StatementQuery()
        {
            this.limit = 25;
        }

        /// <summary>
        /// Returns a query string using the values assigned to this object.
        /// </summary>
        /// <returns></returns>
        public string BuildQueryString()
        {   
            // Start with the basic format
            string query = "?format=exact&";

            // Statement ID only pulls a single statement
            if (this.statementId != null)
            {
                query += this.EncodeString("statementId", this.statementId);
            }
            // Same with Voided Statement ID
            else if (this.voidedStatementId != null)
            {
                query += this.EncodeString("voidedStatementId", this.voidedStatementId);
            }
            // If these are missing, then we'll add the other properties
            else
            {
                // Flags
                query += this.EncodeBool("related_agents", this.relatedAgents);
                query += this.EncodeBool("related_activities", this.relatedActivities);

                // Dates
                query += this.EncodeDate("since", this.sinceDate);
                query += this.EncodeDate("until", this.untilDate);

                // Strings
                query += this.EncodeString("verbId", this.verbId);
                query += this.EncodeString("activityId", this.activityId);
                query += this.EncodeString("registrationId", this.registrationId);
            }

            // Remove any trailing & characters
            if (query.EndsWith("&"))
                query = query.Substring(0, query.Length-1);
            
            return query;
        }

        #region Encoding Helpers
        private string EncodeInt(string name, int? val, string closure = "&")
        {
            if (val == null)
                return "";
            else
                return name + "=" + UnityWebRequest.EscapeURL(val.Value.ToString()) + closure;
        }
        private string EncodeBool(string name, bool? val, string closure = "&")
        {
            if (val == null)
                return "";
            else
                return name + "=" + UnityWebRequest.EscapeURL(val.Value.ToString()) + closure;
        }
        private string EncodeString(string name, string val, string closure = "&")
        {
            if (val == null)
                return "";
            else
                return name + "=" + UnityWebRequest.EscapeURL(val) + closure;
        }
        private string EncodeDate(string name, DateTime? val, string closure = "&")
        {   
            if (val == null)
                return "";
            else
                return name + "=" + UnityWebRequest.EscapeURL(val.Value.ToLongDateString()) + closure;
        }
        #endregion
    }
}
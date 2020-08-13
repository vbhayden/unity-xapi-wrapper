using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace XAPI
{
    /// <summary>
    /// Interface into the XAPI functions contained within this wrapper. 
    /// </summary>
    public sealed class XAPIWrapper
    {
        // Allow users to assign a common Actor for their statements
        private static Actor commonActor;

        /// <summary>
        /// Check that an XAPIMessenger component exists and is ready to send requests.
        /// </summary>
        public static void CheckMessenger ()
        {
            if (XAPIMessenger.InstanceExists == false)
            {
                // Create an empty game object following our convention
                new GameObject ("XAPI").AddComponent<XAPIMessenger> ();
            }
        }

        /// <summary>
        /// Assign an LRS configuration to this wrapper.  This configuration will be used for
        /// all statements sent after assignment.
        /// </summary>
        /// <param name="config"></param>
        public static void AssignConfig (Config config)
        {
            // Make sure our messenger exists
            XAPIWrapper.CheckMessenger ();

            // Relay this to it
            XAPIMessenger.AssignConfig (config);
        }

        /// <summary>
        /// Assign a common actor that will be used for xAPI statements.
        /// 
        /// 
        /// </summary>
        /// <param name="actor"></param>
        public static void AssignCommonActor(Actor actor)
        {
            XAPIWrapper.commonActor = actor;
        }
        
        /// <summary>
        /// Send an array of statements to the configured LRS.
        /// </summary>
        /// <param name="statements">Statements to send.</param>
        /// <param name="callback">Optional callback for once this operation ends.</param>
        public static void SendStatements(Statement[] statements, Action<MultiStatementStoredResponse> callback = null)
        {
            XAPIWrapper.CheckMessenger();
            XAPIMessenger.SendStatements(statements, callback);
        }   


        /// <summary>
        /// Send the given statement to the configured LRS.  
        /// </summary>
        /// <param name="statements">Statement to send.</param>
        /// <param name="callback">Optional callback for once this operation ends.</param>
        public static void SendStatement(Statement statement, Action<StatementStoredResponse> callback = null)
        {
            // Make sure we have a messenger.  Each overload of this function eventually
            // uses this version, so we only need to check for a messenger here.
            //
            XAPIWrapper.CheckMessenger();
            XAPIMessenger.SendStatement(statement, callback);
        }

        /// <summary>
        /// Sends an xAPI statement to the configured LRS.  This requires all necessary part of an xAPI statement.
        /// </summary>
        /// <param name="Actor"></param>
        /// <param name="Verb"></param>
        /// <param name="Object"></param>
        /// <param name="Result"></param>
        public static void SendStatement(Actor Actor, Verb Verb, Activity Activity, Result Result = null, Action<StatementStoredResponse> callback = null)
        {
            var statement = new Statement(Actor, Verb, Activity);

            if (Result != null)
                statement.Result = Result;

            XAPIWrapper.SendStatement(statement, callback);
        }

        /// <summary>
        /// Sends an xAPI statement to the configured LRS.  This version will use the common actor.
        /// 
        /// To assign a common actor, provide an actor to XAPIWrapper.AssignCommonActor().
        /// </summary>
        /// <param name="Actor"></param>
        /// <param name="Verb"></param>
        /// <param name="Object"></param>
        /// <param name="Result"></param>
        public static void SendStatement(Verb Verb, Activity Activity, Result Result = null, Action<StatementStoredResponse> callback = null)
        {
            // Make sure we actually have a common actor
            if (XAPIWrapper.HasCommonActor == false)
                throw new System.NullReferenceException("XAPIWrapper.CommonActor is null.  This value is required for this variation of XAPIWrapper.SendStatement");

            XAPIWrapper.SendStatement(XAPIWrapper.CommonActor, Verb, Activity, Result, callback);
        }   

        /// <summary>
        /// Retrieves a single statement from the configured LRS.
        /// 
        /// As you can't overload a function with identical argument types, this is private with
        /// a slightly different name.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        private static void GetStatement(StatementQuery query, Action<Statement, UnityWebRequest> callback)
        {   
            // Make sure we have a messenger
            XAPIWrapper.CheckMessenger();

            // Build the fully qualified URL
            string url = XAPIMessenger.Config.StatementEndpoint + query.BuildQueryString();;

            XAPIMessenger.GetStatement(url, callback); 
        }
        
        /// <summary>
        /// Retrieves a single statement from the configured LRS.
        /// 
        /// You must supply the statement ID as an argument
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public static void GetStatement(string statementId, Action<Statement, UnityWebRequest> callback)
        {   
            // Build the query
            StatementQuery query = new StatementQuery();
            query.statementId = statementId;

            XAPIWrapper.GetStatement(query, callback); 
        }

        /// <summary>
        /// Retrieves a single statement from the configured LRS.
        /// 
        /// You must supply the statement ID as an argument
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public static void GetVoidedStatement(string voidedStatementId, Action<Statement, UnityWebRequest> callback)
        {   
            // Build the query
            StatementQuery query = new StatementQuery();
            query.voidedStatementId = voidedStatementId;

            XAPIWrapper.GetStatement(query, callback); 
        }
        
        /// <summary>
        /// Retrieves statements from a fully-qualified URL pointing to the configured LRS.
        /// 
        /// This is the most granular version of the call and should only be made by the wrapper itself.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public static void GetStatements(string url, Action<StatementResult, UnityWebRequest> callback)
        {   
            // Make sure we have a messenger
            XAPIWrapper.CheckMessenger();
            XAPIMessenger.GetStatements(url, callback); 
        }

        /// <summary>
        /// Retrieves statements according to the StatementQuery values. 
        /// 
        /// The StatementResult object in the callback may contain a `MoreIRL` value.  If that is the case,
        /// then more statements matching your query can be returned using that endpoints with your LRS.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="callback"></param>
        public static void GetStatements(StatementQuery query, Action<StatementResult, UnityWebRequest> callback)
        { 
            string url = XAPIMessenger.Config.StatementEndpoint + query.BuildQueryString();
            XAPIMessenger.GetStatements(url, callback); 
        }

        /// <summary>
        /// Retrieves statements using the "more" property attached to a previous statement response.
        /// 
        /// This function will remove the first parts of that "more" property to fit your LRS endpoint.
        /// </summary>
        /// <param name="moreIrl"></param>
        /// <param name="callback"></param>
        public static void GetMoreStatements(string moreIrl, Action<StatementResult, UnityWebRequest> callback)
        { 
            // Make sure it's formatted correctly
            if (moreIrl.Contains("/more/"))
            {
                // Find out where it is 
                int index = moreIrl.IndexOf("/more/");
                string more = moreIrl.Substring(index, moreIrl.Length - index);

                // Build the fully qualified URL
                string url = XAPIMessenger.Config.Endpoint + more;

                // Make the request
                XAPIMessenger.GetStatements(url, callback); 
            }
            else
            {
                throw new ArgumentException("XAPIWrapper.GetMoreStatements: moreIrl argument must contain \"/more/\"");
            }
        }

        /// <summary>
        /// Gets the common actor in use by the XAPIWrapper.
        /// </summary>
        /// <value></value>
        public static Actor CommonActor
        {
            get { return XAPIWrapper.commonActor; }
        }

        /// <summary>
        /// Gets whether or not a common actor has been assigned to the wrapper.
        /// </summary>
        /// <value></value>
        public static bool HasCommonActor
        {
            get { return XAPIWrapper.commonActor != null; }
        }
    }
}
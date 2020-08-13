using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace XAPI
{
    public class XAPIMessenger : MonoBehaviour
    {
        // Preserve a single instance of this component
        private static XAPIMessenger instance;
        private Config config;

        // Allow users to assign a configuration object from the inspector
        [SerializeField] private ConfigObject configObject;

        // Called when this component is first created
        void Awake ()
        {
            // Check if we already have an instance live
            if (XAPIMessenger.instance != null)
            {
                GameObject.Destroy (this);
                return;
            }

            // If we didn't, then this is it
            XAPIMessenger.instance = this;
            GameObject.DontDestroyOnLoad (this.gameObject);

            // Check if we were assigned a ConfigObject
            if (this.configObject != null)
            {
                this.config = this.configObject.config;
                Debug.LogFormat("LRS Configuration loaded for: {0}", this.config.Endpoint);
            }
            else
            {
                Debug.LogErrorFormat("LRS CONFIGURATION MISSING FROM XAPI MESSENGER.  YOU MUST ASSIGN A CONFIGURATION.");
            }
        }

        /// <summary>
        /// Check whether or not the XAPIMessenger instance already exists.
        /// </summary>
        /// <value></value>
        public static bool InstanceExists
        {
            get
            {
                return XAPIMessenger.instance != null;
            }
        }

        /// <summary>
        /// Gets the ConfigObject instance attached in the inspector.
        /// </summary>
        /// <value></value>
        public static ConfigObject ConfigObject
        {
            get 
            { 
                if (XAPIMessenger.instance == null)
                    return null;
                return XAPIMessenger.instance.configObject; 
            }
        }

        /// <summary>
        /// Instance-specific config object.
        /// 
        /// This is only exposed to help the editor.  To get the ConfigObject for your scene,
        /// use the static XAPIMessenger.ConfigObject.
        /// </summary>
        /// <value></value>
        public ConfigObject LocalConfigObject
        {
            get 
            { 
                return this.configObject; 
            }
        }

        /// <summary>
        /// Gets the current LRS configuration.
        /// 
        /// Make sure your messenger instance exists before using this.  Config objects
        /// are structs and so this will return the default struct values if this does not
        /// exist yet!
        /// </summary>
        /// <value></value>
        public static Config Config
        {
            get
            { 
                if (XAPIMessenger.instance == null)
                    return new Config();
                
                return XAPIMessenger.instance.config; 
            }
        }

        /// <summary>
        /// Assign an LRS configuration to this messenger.  This should only be called from the XAPIWrapper.
        /// </summary>
        /// <param name="config"></param>
        public static void AssignConfig (Config config)
        {
            XAPIMessenger.instance.config = config;
        }

        #region Sending Statements
        /// <summary>
        /// Sends an xAPI statement.  This accepts a full statement as an argument.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="callback"></param>
        public static void SendStatement(Statement statement, Action<StatementStoredResponse> callback = null)
        {
            XAPIMessenger.instance.StartCoroutine(XAPIMessenger.SendStatementRoutine(statement, callback));
        }
        
        /// <summary>
        /// Sends an xAPI statement.  This accepts a full statement as an argument.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="callback"></param>
        public static void SendStatements(Statement[] statements, Action<MultiStatementStoredResponse> callback = null)
        {
            XAPIMessenger.instance.StartCoroutine(XAPIMessenger.SendMultiStatementRoutine(statements, callback));
        }

        /// <summary>
        /// Coroutine for sending an xAPI Statement.  
        /// 
        /// This version requires a constructed Statement object. 
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator SendStatementRoutine(Statement statement, Action<StatementStoredResponse> callback = null)
        {
            var outboundArray = new Statement[] { statement };

            yield return XAPIMessenger.SendMultiStatementRoutine(outboundArray, res => 
            {
                if (callback != null)
                {
                    callback(new StatementStoredResponse {
                        Request = res.Request,
                        Statement = res.Statements == null ? null : res.Statements[0],
                        StatementID = res.StatementIDs == null ? null : res.StatementIDs[0]
                    }); 
                }
            });
        }

        /// <summary>
        /// Coroutine for sending an xAPI Statement.  
        /// 
        /// This version requires a constructed Statement object. 
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator SendMultiStatementRoutine(Statement[] statements, Action<MultiStatementStoredResponse> callback = null)
        {
            // Serialize this thing then convert into a utf-8 byte array
            var payload = JsonConvert.SerializeObject(statements);
            var request = XAPIMessenger.BuildPostRequest(XAPIMessenger.Config.StatementEndpoint, payload);

            // Yield during the request
            yield return request.SendWebRequest();

            // Use the callback if supplied
            if (callback != null)
            {
                // Check for the statement IDs
                string[] statementKeys = null;

                try 
                {
                    statementKeys = JsonConvert.DeserializeObject<string[]>(request.downloadHandler.text);
                }
                catch (Exception ex)
                {
                    Debug.LogErrorFormat("ERROR: Response Code: {0}", request.responseCode);
                    Debug.LogErrorFormat("ERROR: Could not deserialize LRS response.  Expected JSON string array, received: {0}", request.downloadHandler.text);
                    Debug.LogErrorFormat("ERROR: Exception: {0}", ex.Message);
                }

                callback(new MultiStatementStoredResponse {
                    Request = request,
                    Statements = statements,
                    StatementIDs = statementKeys
                });
            }
        }
        #endregion

        #region Getting Statements

        /// <summary>
        /// Retrieve statements from an LRS.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public static void GetStatements(string url, Action<StatementResult, UnityWebRequest> callback)
        {
            XAPIMessenger.instance.StartCoroutine(XAPIMessenger.GetStatementsRoutine(url, callback));
        }

        /// <summary>
        /// Coroutine for retrieving xAPI statements from the configured LRS.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator GetStatementsRoutine(string url, Action<StatementResult, UnityWebRequest> callback)
        {
            // Serialize this thing then convert into a utf-8 byte array
            var request = XAPIMessenger.BuildGetRequest(url);

            yield return request.SendWebRequest();

            // Return whatever we found.
            StatementResult result = JsonConvert.DeserializeObject<StatementResult>(request.downloadHandler.text);

            // Correct Actor IFI Types
            for (int k=0; k<result.StatementCount; k++)
            {
                result.Statements[k].Actor.ifi = result.Statements[k].Actor.GuessIFI();
            }

            // Make the callback
            callback(result, request);
        }

        /// <summary>
        /// Retrieve statements from an LRS.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public static void GetStatement(string url, Action<Statement, UnityWebRequest> callback)
        {
            XAPIMessenger.instance.StartCoroutine(XAPIMessenger.GetStatementRoutine(url, callback));
        }

        public static IEnumerator GetStatementRoutine(string url, Action<Statement, UnityWebRequest> callback)
        {
            // Serialize this thing then convert into a utf-8 byte array
            var request = XAPIMessenger.BuildGetRequest(url);

            // Yield during the request
            yield return request.SendWebRequest();

            // Return whatever we found.
            Statement statement = JsonConvert.DeserializeObject<Statement>(request.downloadHandler.text);

            // Fix IFI
            statement.Actor.ifi = statement.Actor.GuessIFI();

            // Make the callback
            callback(statement, request);
        }

        #endregion

        /// <summary>
        /// Shared logic for building an LRS-bound POST request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        private static UnityWebRequest BuildPostRequest(string url, string payload)
        {
            // We converted to bytes because Unity does some weird formatting when you provide
            // a json string and the responses aren't quite right.  Oddly enough, POST doesn't 
            // allow you to pass a byte array but PUT does.
            var request = UnityWebRequest.Put(url, payload);

            // Configure the request
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Authorization", XAPIMessenger.Config.BasicAuthFull);
            request.SetRequestHeader("X-Experience-API-Version", "1.0.0");
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }

        private static UnityWebRequest BuildGetRequest(string url)
        {
            var request = UnityWebRequest.Get(url);

            // Configure the request
            request.method = UnityWebRequest.kHttpVerbGET;
            request.SetRequestHeader("Authorization", XAPIMessenger.Config.BasicAuthFull);
            request.SetRequestHeader("X-Experience-API-Version", "1.0.0");
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }
    }

    /// <summary>
    /// Enum representing Authentication methods for an LRS.
    /// </summary>
    public enum LRSAuthMethod
    {
        Basic = 0,
        BasicEncoded = 1,
        //OAuth2 = 2
    }
}
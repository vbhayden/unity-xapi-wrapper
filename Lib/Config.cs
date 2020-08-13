using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace XAPI
{
    [System.Serializable]
    public class Config
    {
        #region Fields
        // Private fields for our properties
        [SerializeField] private LRSAuthMethod authMethod = LRSAuthMethod.Basic;
        [SerializeField] private string endpoint = Config.DEFAULT_ENDPOINT;
        [SerializeField] private string username = Config.DEFAULT_USERNAME;
        [SerializeField] private string password = Config.DEFAULT_PASSWORD;
        [SerializeField] private string basicAuth = Config.DEFAULT_BASIC_AUTH;
        [SerializeField] private int timeout = Config.DEFAULT_TIMEOUT;
        #endregion

        #region Constants and Defaults
        /// <summary>
        /// The generic username to send statements to the ADL LRS.
        /// </summary>
        public const string DEFAULT_USERNAME = "tom";

        /// <summary>
        /// The generic password to send statements to  the ADL LRS.
        /// </summary>
        public const string DEFAULT_PASSWORD = "1234";

        /// <summary>
        /// Base64 encoding of the "username:password" pair.
        /// </summary>
        public const string DEFAULT_BASIC_AUTH = "dG9tOjEyMzQ=";

        /// <summary>
        /// The default timeout in milliseconds for requests to the LRS.
        /// </summary>
        public const int DEFAULT_TIMEOUT = 5000;

        /// <summary>
        /// Default endpoint, points to the ADL LRS.
        /// </summary>
        public const string DEFAULT_ENDPOINT = "https://lrs.adlnet.gov/xapi/";
        #endregion

        #region Functions
        /// <summary>
        /// Default constructor for serialization, DO NOT USE.
        /// 
        /// Sets everything to use the ADL LRS by default
        /// </summary>
        public Config () { }

        /// <summary>
        /// Returns an LRS configuration using Basic Auth.
        /// </summary>
        /// <param name="url">LRS Endpoint with a trailing slash (will add one if missing)</param>
        /// <param name="user">LRS Username</param>
        /// <param name="pass">LRS Password</param>
        /// <param name="time">Request Timeout</param>
        /// <returns></returns>
        public static Config WithBasicAuth (string endpoint, string username, string password, int timeout = DEFAULT_TIMEOUT)
        {
            return new Config ()
            {
                AuthMethod = LRSAuthMethod.Basic,
                Endpoint = endpoint,
                Username = username,
                Password = password,
                Timeout = timeout
            };
        }

        /// <summary>
        /// Returns an LRS configuration using a pre-encoded Basic Auth of form base64("username:password")
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="basicAuth"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static Config WithBasicAuthEncoded (string endpoint, string basicAuth, int timeout = DEFAULT_TIMEOUT)
        {
            return new Config ()
            {
                AuthMethod = LRSAuthMethod.BasicEncoded,
                Endpoint = endpoint,
                BasicAuth = basicAuth,
                Timeout = timeout
            };
        }

        /// <summary>
        /// Assign all default values for this Config instance.
        /// </summary>
        public void AssignDefaults()
        {
            this.authMethod = LRSAuthMethod.Basic;
            this.endpoint = Config.DEFAULT_ENDPOINT;
            this.username = Config.DEFAULT_USERNAME;
            this.password = Config.DEFAULT_PASSWORD;
            this.basicAuth = Config.DEFAULT_BASIC_AUTH;
            this.timeout = Config.DEFAULT_TIMEOUT;
        }
        #endregion

        #region Property Definitions
        /// <summary>
        /// Gets or sets the timeout for LRS requests.
        /// </summary>
        /// <value>The timeout.</value>
        public int Timeout
        {
            get { return this.timeout; }
            set { this.timeout = value; }
        }

        /// <summary>
        /// The intended authentication method that you will use for this LRS.  
        /// 
        /// Currently, only Basic and Basic (Pre-Endoded) are available.
        /// </summary>
        public LRSAuthMethod AuthMethod
        {
            get { return this.authMethod; }
            set { this.authMethod = value; }
        }

        /// <summary>
        /// Endpoint being used by the desired LRS.  This is usually a URL ending in "/xapi/".  Points to the ADL LRS by default.
        /// 
        /// For this wrapper, the URL MUST end in a slash.  If not, then the value will be modified to include it.
        /// </summary>
        /// <value></value>
        public string Endpoint
        {
            get
            {
                return this.endpoint;
            }
            set
            {
                if (this.endpoint != value)
                    this.endpoint = value;

                if (this.endpoint.EndsWith ("/") == false)
                    this.endpoint = this.endpoint + "/";
            }
        }

        /// <summary>
        /// Username for Basic authentication.
        /// </summary>
        /// <value></value>
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
        /// <summary>
        /// Password for Basic authentication.
        /// </summary>
        /// <value></value>
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        /// <summary>
        /// Base64-encoded string to be used literally when communicating with the LRS.
        /// 
        /// This MUST NOT include the string "Basic " as this will be added by the wrapper.
        /// </summary>
        /// <value></value>
        public string BasicAuth
        {
            get 
            {
                return this.basicAuth; 
            }
            set
            {
                // Check if this is even valid
                string decoded;
                bool valid = XAPIUtil.TryParseBase64 (value, out decoded);

                // Make sure it decoded at all
                if (valid == false)
                    Debug.LogErrorFormat ("XAPI.Config.BasicAuth was assigned non-Base64 string: {0}", value);

                // Make sure it at least had a colon
                else if (decoded.Contains (":") == false)
                    Debug.LogErrorFormat ("Decoded XAPI.Config.BasicAuth assignment did not contain a ':' character: {0}", decoded);

                this.basicAuth = value;
            }
        }
        
        /// <summary>
        /// Gets the full string for Basic Auth, including "Basic "
        /// </summary>
        /// <value></value>
        public string BasicAuthFull
        {
            get 
            {
                if (this.AuthMethod == LRSAuthMethod.Basic)
                    return "Basic " + XAPIUtil.ToBase64(this.username + ":" + this.password);
                else
                    return "Basic " +  this.BasicAuth;
            }
        }

        /// <summary>
        /// Endpoint for statements from the given LRS.  "/statements"
        /// </summary>
        /// <value></value>
        public string StatementEndpoint
        {
            get { return this.Endpoint + "statements"; }
        }
        /// <summary>   
        /// Endpoint for state resources from the given LRS.  "/activities/state"
        /// </summary>
        /// <value></value>
        public string StateResourceEndpoint
        {
            get { return this.Endpoint + "activities/state"; }
        }
        /// <summary>   
        /// Endpoint for agent resources from the given LRS.  "/agents"
        /// </summary>
        /// <value></value>
        public string AgentEndpoint
        {
            get { return this.Endpoint + "agents"; }
        }
        /// <summary>   
        /// Endpoint for agent profile resources from the given LRS.  "/agents"
        /// </summary>
        /// <value></value>
        public string AgentProfileEndpoint
        {
            get { return this.Endpoint + "agents/profile"; }
        }
        /// <summary>   
        /// Endpoint for activity resources from the given LRS.  "/agents"
        /// </summary>
        /// <value></value>
        public string ActivityEndpoint
        {
            get { return this.Endpoint + "activities"; }
        }
        /// <summary>   
        /// Endpoint for activity profile resources from the given LRS.  "/agents"
        /// </summary>
        /// <value></value>
        public string ActivityProfileEndpoint
        {
            get { return this.Endpoint + "activities/profile"; }
        }
        /// <summary>   
        /// Endpoint for information about the LRS itself.  "/about"
        /// </summary>
        /// <value></value>
        public string AboutEndpoint
        {
            get { return this.Endpoint + "about"; }
        }
        #endregion
    }
}
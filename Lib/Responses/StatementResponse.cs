using UnityEngine;
using UnityEngine.Networking;

namespace XAPI
{
    /// <summary>
    /// Callback argument for single-statement submissions to an LRS.
    /// </summary>
    public class StatementStoredResponse
    {
        /// <summary>
        /// The ID assigned to the statement being stored.
        /// </summary>
        /// <value></value>
        public string StatementID { get; set; }

        /// <summary>
        /// The Unity request object used for this interaction. 
        /// </summary>
        /// <value></value>
        public UnityWebRequest Request { get; set; }

        /// <summary>
        /// The statement we tried to submit.
        /// </summary>
        /// <value></value>
        public Statement Statement { get; set; }
    }
}
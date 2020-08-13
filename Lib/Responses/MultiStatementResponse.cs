using UnityEngine;
using UnityEngine.Networking;

namespace XAPI
{
    /// <summary>
    /// Callback argument for multi-statement submissions to an LRS.
    /// </summary>
    public class MultiStatementStoredResponse
    {
        /// <summary>
        /// The ID assigned to the statement being stored.
        /// </summary>
        /// <value></value>
        public string[] StatementIDs { get; set; }

        /// <summary>
        /// The Unity request object used for this interaction. 
        /// </summary>
        /// <value></value>
        public UnityWebRequest Request { get; set; }

        /// <summary>
        /// The statement we tried to submit.
        /// </summary>
        /// <value></value>
        public Statement[] Statements { get; set; }
    }
}
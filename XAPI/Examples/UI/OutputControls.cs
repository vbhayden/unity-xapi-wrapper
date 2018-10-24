using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace XAPI.Examples.UI
{
    public class OutputControls : MonoBehaviour
    {
        // Controls
        [SerializeField] private Text textLog;
        [SerializeField] private Button buttonClear;

        // Manager
        [SerializeField] private UIExampleManager manager;

        void Awake()
        {
            this.buttonClear.onClick.AddListener(this.ClearLog);
        }

        /// <summary>
        /// Clear the log.
        /// </summary>
        public void ClearLog()
        {
            this.textLog.text = "";
        }

        /// <summary>
        /// Append an xAPI statement to the log
        /// </summary>
        /// <param name="statementKey"></param>
        /// <param name="statement"></param>
        public void AppendLog (Statement statement)
        {
            this.textLog.text += string.Format("\n{0}", statement.SimpleTriple);
        }
        
        /// <summary>
        /// Append plain text to the log.
        /// </summary>
        /// <param name="text"></param>
        public void AppendLog (string text)
        {
            this.textLog.text += string.Format("\n{0}", text);
        }
    }
}
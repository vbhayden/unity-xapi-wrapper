using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace XAPI.Examples.Simple
{
    public class SimpleExample : MonoBehaviour
    {
        // Constants
        public const string EXAMPLE_DOMAIN = "http://example.com";

        // Our Button
        public Button button;

        // xAPI Objects
        private XActor actor;
        private XActivity activity;

        // Use this for initialization
        void Awake ()
        {   
            // Assign our callback
            this.button.onClick.AddListener(this.OnButtonClick);

            // Create our objects
            this.actor = XActor.FromAccount(EXAMPLE_DOMAIN, "some-key-relevant-to-homepage", false, "Example Actor Name");
            this.activity = new XActivity(EXAMPLE_DOMAIN + "/simple-example", "Simple Unity Example");
        }
        
        // Simple button callback
        void OnButtonClick ()
        {
            XAPIWrapper.SendStatement(this.actor, XVerbs.Interacted, this.activity, callback: this.SendStatementCallback);
        }

        // Simple xAPI Callback
        void SendStatementCallback(Statement statement, string statementKey, UnityWebRequest request)
        {
            if (request.responseCode == 200)
                Debug.LogFormat("Sent Statement: {0}, stored with key: {1}", statement.SimpleTriple, statementKey);
            else
            {
                Debug.LogErrorFormat("ERROR (part 1 of 3): Check your LRS configuration and statement syntax!");
                Debug.LogErrorFormat("ERROR (part 2 of 3): Statement: {0}", statement.SimpleTriple);
                Debug.LogErrorFormat("ERROR (part 2 of 3): Response: {0}", request.downloadHandler.text);
            }
        }
    }
}
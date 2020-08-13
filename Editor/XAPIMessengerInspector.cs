using UnityEditor;
using UnityEngine;

namespace XAPI
{
    [CustomEditor(typeof(XAPIMessenger))]
    public class XAPIMessengerInspector : Editor {

        // Instance of this script we're using
        private XAPIMessenger messenger;

        public override void OnInspectorGUI()
        {
            // Get our component
            this.messenger = (target as XAPIMessenger);

            // Tell people what it does
            EditorGUILayout.HelpBox(
                "Primary component when using this Unity xAPI Wrapper." +
                "\n\nThis component will send web requests using Unity's coroutine system and its cross-platform Web Request library." + 
                "\n\nIf this component does not exist when needed, the Wrapper will create one."
                , MessageType.Info
            );

            // Expose the private ConfigObject field
            base.OnInspectorGUI();

            GUI.enabled = false;
            EditorGUI.indentLevel = 2;

            // Make sure we have a configuration
            if (this.messenger.LocalConfigObject != null)
            {
                // Sanity
                var config = this.messenger.LocalConfigObject.config;
                
                EditorGUILayout.TextField("-LRS Endpoint", config.Endpoint);
                EditorGUILayout.IntField("-Timeout (s)", config.Timeout);

                // The main area of interest is changing how we authenticate with the LRS
                switch(config.AuthMethod)
                {
                    default:
                    case LRSAuthMethod.Basic:
                        EditorGUILayout.TextField("-Username", config.Username);
                        EditorGUILayout.TextField("-Password", config.Password);
                        break;

                    case LRSAuthMethod.BasicEncoded:
                        EditorGUILayout.TextField("-Basic Auth", config.BasicAuth);
                        break;
                }

                // Just show the values based on their auth method
            }
            // Warn them if we haven't gotten a configuration yet...
            else
            {
                EditorGUILayout.HelpBox(
                    "An LRS configuration has not been supplied!" +
                    "\n\nYou must supply an LRS configuration to use this library." + 
                    "\n\nRight-click within the inspector and select:" + 
                    "\n\n     Create > XAPI > New LRS Configuration"
                    , MessageType.Error
                );
            }

            GUI.enabled = true;
            EditorGUI.indentLevel = 0;
        }
    }
}

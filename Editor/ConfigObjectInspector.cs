using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace  XAPI
{
    [CustomEditor(typeof(ConfigObject))]
    public class ConfigObjectInspector : Editor {

        // Instance of this script we're using
        private ConfigObject configObject;
        private Config config;

        public override void OnInspectorGUI()
        {
            // Get our component
            this.configObject = (target as ConfigObject);
            this.config = this.configObject.config;

            // Check for changes
            EditorGUI.BeginChangeCheck();

            // Describe the endpoint
            EditorGUILayout.HelpBox(
                "The endpoint is a URL that determines which LRS will be receiving the xAPI statements.  \n\n" + 
                "This usually ends in \"/xapi/\", although that convention isn't mandated by the specification."
                , MessageType.None
            );

            // LRS Endpoint
            this.config.Endpoint = EditorGUILayout.TextField("Endpoint", this.config.Endpoint);

            // Select the Authorization Method
            GUILayout.Space(20);
            EditorGUILayout.HelpBox(
                "The authorization method determines how this library will send your LRS credentials.  \n\n" +
                "As different methods require different credentials, this will change the subsequent fields."
                , MessageType.None
            );
            this.config.AuthMethod = (LRSAuthMethod) EditorGUILayout.EnumPopup("Auth Method", this.config.AuthMethod);

            // Move in for the auth-specific fields
            EditorGUI.indentLevel = 2;

            // The main area of interest is changing how we authenticate with the LRS
            switch(this.config.AuthMethod)
            {
                default:
                case LRSAuthMethod.Basic:

                    GUILayout.Space(20);
                    EditorGUILayout.HelpBox(
                        "Basic authorization requires a username and password.  Use whichever credentials you use to log into your LRS. "
                        , MessageType.None
                    );
                    this.config.Username = EditorGUILayout.TextField("Username", this.config.Username);
                    this.config.Password = EditorGUILayout.TextField("Password", this.config.Password);
                    break;

                case LRSAuthMethod.BasicEncoded:

                    GUILayout.Space(20);
                    EditorGUILayout.HelpBox(
                        "If your LRS provides a generic, pre-encoded basic auth string, then use that here."
                        , MessageType.None
                    );
                    this.config.BasicAuth = EditorGUILayout.TextField("Basic Auth", this.config.BasicAuth);
                    break;
            }

            // Scoot back out
            EditorGUI.indentLevel = 0;
        
            GUILayout.Space(20);
            EditorGUILayout.HelpBox(
                "Number of seconds to wait before an LRS-bound web request will time out."
                , MessageType.None
            );
            this.config.Timeout = EditorGUILayout.IntField("Timeout (seconds)", this.config.Timeout);

            // Dirty the scene if things changed
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(this.configObject);

                // Get the current scene we're using
                var currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ();
                
                // Make it dirty (or save will ignore it) and then force a save call
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (currentScene);
            }
        }
    }
}

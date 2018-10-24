using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI.Examples.UI
{
    public class UIExampleManager : MonoBehaviour
    {
        // Constants
        public const string PLAYER_PREF_STATE_KEY = "XAPI_UI_EXAMPLE_STATE";
        public const string STATE_KEY_SLIDER = "XAPI_UI_EXAMPLE_SLIDER";
        public const string STATE_KEY_TOGGLE = "XAPI_UI_EXAMPLE_TOGGLE";
        public const string STATE_KEY_TEXT_BOX = "XAPI_UI_EXAMPLE_TEXT_BOX";

        public const string XAPI_ACTIVITY_IRI_BASE = "http://example.com/unity-wrapper/ui-example";
        public const string XAPI_RESULT_EXTENSION = XAPI_ACTIVITY_IRI_BASE + "/data";

        // Helpers
        [SerializeField] private SaveLoadControls saveLoadControls;
        [SerializeField] private UIControls uiControls;
        [SerializeField] private OutputControls outputControls;

        /// <summary>
        /// Updates the scene using data stored in an xAPI statement.
        /// </summary>
        /// <param name="statement"></param>
        public void UpdateSceneState(Statement statement)
        {
            // Make sure we even have a result
            if (statement.MainResult == null)
                return;
                
            // Parse the SaveData object here
            object data;

            if (statement.MainResult.Extensions.TryGetValue(UIExampleManager.XAPI_RESULT_EXTENSION, out data)) 
            {
                var state = JsonConvert.DeserializeObject<SaveData>(data.ToString());
                this.uiControls.LoadSceneState(state);
            }
        }

        /// <summary>
        /// Returns the current scene state as a 
        /// </summary>
        /// <returns></returns>
        public SaveData GetSceneState()
        {
            return this.uiControls.GetSceneState();
        }

        /// <summary>
        /// Write to the Output Log.
        /// </summary>
        /// <param name="text"></param>
        public void AppendLog(string text)
        {
            this.outputControls.AppendLog(text);
        }

        /// <summary>
        /// Write to the Output Log.
        /// </summary>
        /// <param name="text"></param>
        public void AppendLog(Statement statement)
        {
            this.outputControls.AppendLog(statement);
        }
    }
}
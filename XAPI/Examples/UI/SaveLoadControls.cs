using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace XAPI.Examples.UI
{
    public class SaveLoadControls : MonoBehaviour
    {
        // UI objects
        [SerializeField] private InputField inputActorId;
        [SerializeField] private InputField inputActorHome;
        [SerializeField] private InputField inputActorName;
        [SerializeField] private Button buttonSave;
        [SerializeField] private Button buttonLoad;

        // Activity
        private XActivity mainActivity;

        // Manager
        [SerializeField] private UIExampleManager manager;

        void Awake ()
        {
            this.buttonLoad.onClick.AddListener (this.LoadPrevious);
            this.buttonSave.onClick.AddListener (this.SaveState);

            // Our activity
            this.mainActivity = new XActivity(UIExampleManager.XAPI_ACTIVITY_IRI_BASE, "UI Example");

            // Check if we even did this before
            this.buttonLoad.enabled = PlayerPrefs.HasKey(UIExampleManager.PLAYER_PREF_STATE_KEY);

            if (this.buttonLoad.enabled)
            {
                this.LoadPrevious();
            }

            // Assign our common actor
            var actor = XActor.FromAccount(this.inputActorHome.text, this.inputActorId.text, false, this.inputActorName.text);
            XAPIWrapper.AssignCommonActor(actor);
        }

        private void LoadPrevious ()
        {
            // We're only enabling this button if a statement key exists, so get it
            string key = PlayerPrefs.GetString(UIExampleManager.PLAYER_PREF_STATE_KEY);

            // Get the statement corresponding to our last state
            XAPIWrapper.GetStatement(key, this.GetStatementCallback);
        }

        private void SaveState ()
        {
            // Get the current scene state for our controls
            var state = this.manager.GetSceneState();

            // Build a statement from our UI values
            var actor = XActor.FromAccount(this.inputActorHome.text, this.inputActorId.text, false, this.inputActorName.text);
            var statement = new Statement(actor, XVerbs.Suspended, this.mainActivity);

            // Add our data
            statement.MainResult = new XResult();
            statement.MainResult.Extensions.Add(UIExampleManager.XAPI_RESULT_EXTENSION, state);

            // Log it
            this.manager.AppendLog(statement);

            // Send the statement
            XAPIWrapper.SendStatement(statement, this.SendStatementCallback);

            // Save this as our common actor
            XAPIWrapper.AssignCommonActor(actor);
        }

        private void GetStatementCallback(Statement statement, UnityWebRequest request)
        {
            // Use this statement's actor to populate our actor fields
            this.inputActorHome.text = statement.MainActor.Account.homePage;
            this.inputActorId.text = statement.MainActor.Account.name;
            this.inputActorName.text = statement.MainActor.Name;

            // Then let the manager do the rest
            this.manager.UpdateSceneState(statement);

            // Write this to the log
            this.manager.AppendLog("Loaded Previous State!");
        }

        private void SendStatementCallback(Statement statement, string statementKey, UnityWebRequest request)
        {
            // Check our response
            if (request.responseCode == 200) 
            {
                // Write this to the log
                this.manager.AppendLog("Saved State!");

                // Then update the PlayerPref value
                PlayerPrefs.SetString(UIExampleManager.PLAYER_PREF_STATE_KEY, statementKey);
                this.buttonLoad.enabled = true;
                
            }
            else
                this.manager.AppendLog("Error sending statement: " + request.downloadHandler.text);
        }
    }
}
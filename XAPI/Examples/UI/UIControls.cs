using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace XAPI.Examples.UI
{
    public class UIControls : MonoBehaviour
    {
        // Controls
        [SerializeField] private Toggle toggleControl;
        [SerializeField] private Slider sliderControl;
        [SerializeField] private InputField inputControl;

        [SerializeField] private Toggle verboseControl;

        // Activities for these controls
        private XActivity toggleActivity;
        private XActivity sliderActivity;
        private XActivity inputActivity;

        // Manager
        [SerializeField] private UIExampleManager manager;

        void Awake ()
        {
            // Callback assignment
            this.toggleControl.onValueChanged.AddListener(this.OnToggleChanged);
            this.sliderControl.onValueChanged.AddListener(this.OnSliderChanged);
            this.inputControl.onValueChanged.AddListener(this.OnTextChanged);

            // Activity assignment
            this.toggleActivity = new XActivity(UIExampleManager.XAPI_ACTIVITY_IRI_BASE + "/toggle", "Toggle Control");
            this.sliderActivity = new XActivity(UIExampleManager.XAPI_ACTIVITY_IRI_BASE + "/slider", "Slider Control");
            this.inputActivity = new XActivity(UIExampleManager.XAPI_ACTIVITY_IRI_BASE + "/input", "Input Control");
        }

        /// <summary>
        /// Callback for the Slider's OnValueChanged event.
        /// </summary>
        /// <param name="value"></param>
        void OnSliderChanged(float value)
        {
            if (this.verboseControl.isOn == false)
                return;

            var result = new XResult();
            result.Score = new XScore(this.sliderControl.value, this.sliderControl.minValue, this.sliderControl.maxValue);

            XAPIWrapper.SendStatement(XVerbs.Interacted, this.sliderActivity, result, this.UIControlStatementCallback);
        }

        /// <summary>
        /// Callback for the InputField's OnValueChanged event.
        /// </summary>
        /// <param name="text"></param>
        void OnTextChanged(string text)
        {
            if (this.verboseControl.isOn == false)
                return;

            var result = new XResult();
            result.Response = text;

            XAPIWrapper.SendStatement(XVerbs.Interacted, this.inputActivity, result, this.UIControlStatementCallback);
        }

        /// <summary>
        /// Callback for the Toggle's OnValueChanged event.
        /// </summary>
        /// <param name="value"></param>
        void OnToggleChanged(bool value)
        {
            if (this.verboseControl.isOn == false)
                return;

            var result = new XResult(value);

            XAPIWrapper.SendStatement(XVerbs.Interacted, this.toggleActivity, result, this.UIControlStatementCallback);
        }

        /// <summary>
        /// Simple callback for sending these highly verbose statements through UI controls.
        /// 
        /// This just writes their info to the log.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="statementKey"></param>
        /// <param name="request"></param>
        void UIControlStatementCallback(Statement statement, string statementKey, UnityWebRequest request)
        {
            this.manager.AppendLog(statement);
        }

        /// <summary>
        /// Copy the current scene state 
        /// </summary>
        /// <returns></returns>
        public SaveData GetSceneState ()
        {
            return new SaveData(this.toggleControl.isOn, this.sliderControl.value, this.inputControl.text);
        }

        /// <summary>
        /// Loads the given state into the scene.
        /// </summary>
        /// <param name="state"></param>
        public void LoadSceneState(SaveData state)
        {
            this.toggleControl.isOn = state.ToggleValue;
            this.sliderControl.value = state.SliderValue;
            this.inputControl.text = state.TextValue;
        }
    }
}
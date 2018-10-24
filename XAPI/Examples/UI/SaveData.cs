using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;

namespace XAPI.Examples.UI
{   
    /// <summary>
    /// To complicate the example into a more realistic situation, we'll make our own data object 
    /// with three simple values.  This object will be appended to the xAPI statement and preserve
    /// its structure throughout the transaction from client -> LRS -> client.
    /// </summary>
    [System.Serializable]
	public class SaveData
	{
		/// <summary>
		/// Gets the ID declared during construction.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("toggle", NullValueHandling = NullValueHandling.Ignore)]
		public bool ToggleValue
		{
			get
			{
				return this.toggleValue;
			}
            set 
            {
                this.toggleValue = value;
            }
		}

        [JsonProperty("slider", NullValueHandling = NullValueHandling.Ignore)]
		public float SliderValue
		{
			get
			{
				return this.sliderValue;
			}
            set 
            {
                this.sliderValue = value;
            }
		}

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
		public string TextValue
		{
			get
			{
				return this.textValue;
			}
            set 
            {
                this.textValue = value;
            }
		}

		private float sliderValue;
		private bool toggleValue;
		private string textValue;

		/// <summary>
		/// This is intended only for JSON, do not use manually.
		/// </summary>
		[JsonConstructor]
		public SaveData()
		{
			this.sliderValue = 0;
            this.toggleValue = false;
            this.textValue = "";
		}

        /// <summary>
        /// Manual version of the SaveData constructor.  All values are required for this one.
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="slider"></param>
        /// <param name="text"></param>
        public SaveData(bool toggle, float slider, string text)
        {
            this.sliderValue = slider;
            this.toggleValue = toggle;
            this.textValue = text;
        }
    }
}
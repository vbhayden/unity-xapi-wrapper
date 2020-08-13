using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XAPI
{
	/// <summary>
	/// Custom converter for handling nested JSON parsing.  Taken from:
	/// https://stackoverflow.com/questions/6416017/json-net-deserializing-nested-dictionaries
	/// 
	/// This can used as a last resort if a json value being returned is not uniformly
	/// declared and can change shape across instances -- namely the State and Profile resources.
	/// </summary>
	class NestedJsonConverter : CustomCreationConverter<IDictionary<string, object>>
	{
		public override IDictionary<string, object> Create(Type objectType)
		{
			return new Dictionary<string, object>();
		}

		public override bool CanConvert(Type objectType)
		{
			// in addition to handling IDictionary<string, object>
			// we want to handle the deserialization of dict value
			// which is of type object
			return objectType == typeof(object) || base.CanConvert(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject
			    || reader.TokenType == JsonToken.Null)
				return base.ReadJson(reader, objectType, existingValue, serializer);

			// if the next token is not an object
			// then fall back on standard deserializer (strings, numbers etc.)
			return serializer.Deserialize(reader);
		}

		/// <summary>
		/// Convert the specified json into a nested dictionary of string -> object pairings.
		/// 
		/// The objects may be primatives or collections.  It is up to the user to determine how to
		/// handle them after the parsing is finished.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IDictionary<string, object> Convert(string json)
		{
			// Then supply this to the Newtonsoft call as an optional parameter.
			return JsonConvert.DeserializeObject<IDictionary<string, object>>(json, new JsonConverter[] { new NestedJsonConverter() });
		}
	}
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

using Newtonsoft.Json;
using UnityEngine;

namespace XAPI
{
    public static class XAPIUtil
    {
        /// <summary>
        /// Version of the XAPI that we're using here.
        /// </summary>
        public const string XAPI_VERSION = "1.0.0";

        /// <summary>
        /// Converts a given date to an ISO string  used by the wrapper.
        /// </summary>
        /// <returns>The ISO string.</returns>
        /// <param name="date">Date.</param>
        public static string ToISOString (this DateTime date)
        {
            // Get the UTC representation
            DateTime utc = date.ToUniversalTime ();

            // Get our individual units
            int year = utc.Year;
            int month = utc.Month;
            int day = utc.Day;
            int hour = utc.Hour;
            int mins = utc.Minute;
            int secs = utc.Second;
            int ms = utc.Millisecond;

            // From here, we follow the given ISO format
            string iso = string.Format ("{0:00}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}.{6:000}Z", year, month, day, hour, mins, secs, ms);
            return iso;
        }

        /// <summary>
        /// Tries the parse the given ISO DateTime String.
        /// </summary>
        /// <returns>The parse IS.</returns>
        /// <param name="iso">Iso.</param>
        /// <param name="result">Result.</param>
        public static bool TryParseISO (string iso, out DateTime result)
        {
            return DateTime.TryParseExact (iso, "yyyy-MM-ddTHH:mm:ss.fffZ", null, DateTimeStyles.RoundtripKind, out result);
        }

        /// <summary>
        /// Gets the current time in ISO Format.
        /// </summary>
        /// <value>The current time IS.</value>
        public static string CurrentTimeISO
        {
            get
            {
                return XAPIUtil.ToISOString (DateTime.Now);
            }
        }

        /// <summary>
        /// Converts the given text to Base 64.
        /// </summary>
        /// <returns>The base64.</returns>
        /// <param name="text">Text.</param>
        public static string ToBase64 (this string text, System.Text.Encoding encoding = null)
        {
            // Default to ASCII
            encoding = (encoding == null) ? System.Text.Encoding.ASCII : encoding;

            // We needed the encoding to know byte count stored per character, used here
            byte[] textAsBytes = encoding.GetBytes (text);

            // Once we have that, we can just use the Convert call
            return Convert.ToBase64String (textAsBytes);
        }

        /// <summary>
        /// Attempts to decode a Base64 encoded string
        /// </summary>
        /// <returns><c>true</c>, if parse base64 was tryed, <c>false</c> otherwise.</returns>
        /// <param name="encodedText">Encoded text.</param>
        /// <param name="decodedText">Decoded text.</param>
        /// <param name="encoding">Encoding.</param>
        public static bool TryParseBase64 (this string encodedText, out string decodedText, System.Text.Encoding encoding = null)
        {
            // Do this in as basic a fasion as possible
            try
            {
                // Default to ASCII
                encoding = (encoding == null) ? System.Text.Encoding.ASCII : encoding;

                // Similar to the above, we needed encoding to know byte count
                byte[] textAsBytes = Convert.FromBase64String (encodedText);
                decodedText = encoding.GetString (textAsBytes);

                // If we're still here, everything went fine.
                return true;
            }
            catch (Exception ex)
            {
                // Write exception to the log
                Debug.LogWarningFormat("XAPIUtil.TryParseBase64: Failed to decode string: {0}\nException: {1}", encodedText, ex.Message);

                // If we had trouble decoding that for whatever reason
                decodedText = null;
                return false;
            }
        }

        /// <summary>
        /// Determines if the specified obj is a DateTime object.
        /// 
        /// This function is silly and serves only to help with a verbatim translation of the JS wrapper.
        /// </summary>
        /// <returns><c>true</c> if is date the specified obj; otherwise, <c>false</c>.</returns>
        /// <param name="obj">Object.</param>
        public static bool IsDate (object obj)
        {
            return obj is DateTime;
        }

        /// <summary>
        /// Converts the name value collection into a query string.
        /// 
        /// Taken from:
        /// https://stackoverflow.com/questions/829080/how-to-build-a-query-string-for-a-url-in-c
        /// 
        /// </summary>
        /// <returns>The query string.</returns>
        /// <param name="arguments">Arguments.</param>
        public static string ToQueryString (this NameValueCollection arguments)
        {
            // This is a bit uglier than the original, but it doesn't rely on HttpUtility,
            // which is not going to be available atm.
            //
            StringBuilder sb = new StringBuilder ("?");

            // This will check whether to add the '&'
            bool first = true;

            // Go through each key here
            for (int k = 0; k < arguments.Count; k++)
            {
                // Key we're iterating over.  In NameValueCollections, a single key can map
                // to multiple values, so we need to check everything
                //
                string key = arguments.AllKeys[k];
                string[] vals = arguments.GetValues (key);

                for (int v = 0; v < vals.Length; v++)
                {
                    // Get the iterated value
                    string val = vals[v];

                    // If we've seen more than one, we need to join them with the &
                    if (!first)
                        sb.Append ("&");

                    // Then add the typical key=value format
                    sb.AppendFormat ("{0}={1}", Uri.EscapeDataString (key), Uri.EscapeDataString (val));
                    first = false;
                }
            }

            return sb.ToString ();
        }
    }
}
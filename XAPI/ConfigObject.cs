using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace XAPI
{
    /// <summary>
    /// ScriptableObject carrying an LRS configuration.
    /// </summary>
    public class ConfigObject : ScriptableObject {

        public Config config;


#if UNITY_EDITOR
        /// <summary>
        /// Creates a new LRS configuration wherever the user clicked. 
        /// 
        /// This will then select it in the editor so that the user can begin editing it.
        /// </summary>
        [MenuItem("Assets/Create/XAPI/New LRS Configuration")]
        static void CreateNew()
        {   
            // Find out where we clicked
            var path = AssetDatabase.GetAssetPath(Selection.activeObject) + "/New LRS Configuration.asset";

            // Create the asset and write it to that path
            var asset = ScriptableObject.CreateInstance<ConfigObject>();

            AssetDatabase.CreateAsset(asset, path);
        }
#endif
    }
}
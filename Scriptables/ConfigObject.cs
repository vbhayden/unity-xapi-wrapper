using UnityEngine;

namespace XAPI
{
    /// <summary>
    /// ScriptableObject carrying an LRS configuration.
    /// </summary>
    [CreateAssetMenu(menuName = "xAPI/LRS Configuration", fileName = "New LRS Configuration")]
    public class ConfigObject : ScriptableObject 
    {
        public Config config;
    }
}
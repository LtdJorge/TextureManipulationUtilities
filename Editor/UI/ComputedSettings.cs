using UnityEditor;
using UnityEngine;

namespace TextureManipulationUtilities.Editor.UI
{
    [CreateAssetMenu(menuName = "TMU Settings Asset")]
    public class ComputedSettings : ScriptableObject
    {
        public TextureFormat tex1Format;
        public TextureFormat tex2Format;
        public bool tex1EqualsTex2;
    
        [InitializeOnLoadMethod]
        public static void RegisterConverters(){}
    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TextureManipulationUtilities.UI
{
    public class AlphaBlender : EditorWindow
    {
        private VisualTreeAsset mVisualTreeAsset = default;

        [MenuItem("Tools/TMU/AlphaBlender")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<AlphaBlender>();
            wnd.titleContent = new GUIContent("AlphaBlender");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            VisualElement labelFromUXML = mVisualTreeAsset.Instantiate();
            root.Add(labelFromUXML);
        }

        public void OnEnable()
        {
            mVisualTreeAsset = Resources.Load<VisualTreeAsset>("AlphaBlender");
        }
    }
}

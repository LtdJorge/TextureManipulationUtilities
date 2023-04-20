using TextureManipulationUtilities.Editor.Util;
using UnityEditor;
using UnityEngine;

namespace TextureManipulationUtilities.Editor
{
    public class Test : EditorWindow
    {
        private Texture2D tex;
        private static EditorWindow window;
        private Vector2 scrollPos;
        private bool texHasAlpha;

        [MenuItem("Tools/Test")]
        public static void ShowWindow()
        {
            window = GetWindow(typeof(Test), false);
        }

        private void OnGUI()
        {
            if (window)
            {
                GUILayout.BeginArea(new Rect(0, 0, window.position.size.x, window.position.size.y));
                GUILayout.BeginVertical();
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
            }
            GUILayout.Space(20f);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            tex = (Texture2D)EditorGUILayout.ObjectField("Normal Map", tex, typeof(Texture2D), false);
            if (GUILayout.Button("Press"))
            {
                texHasAlpha = HasAlpha.hasAlpha(tex);
            }
            if (window)
            {
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }
    }
}

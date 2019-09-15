using UnityEditor;
using UnityEngine;

namespace Editor.TextureManipulationUtilities
{
    public class TextureChannelInverter : EditorWindow
    {
        private Texture2D textureMap;

        private Vector2 scrollPos;
        private static EditorWindow window;
        private Channels channel;
        private bool saveToDifferentTexture;

        private enum Channels
        {
            Red,
            Green,
            Blue,
            Alpha
        }

        [MenuItem("Tools/NormalMapInverter")]
        public static void ShowWindow()
        {
            window = GetWindow(typeof(TextureChannelInverter), false);
        }

        private void OnInspectorUpdate()
        {
            if (!window)
            {
                window = GetWindow(typeof(TextureChannelInverter), false);
            }
        }

        private void OnGUI()
        {
            if (window)
            {
                GUILayout.BeginArea(new Rect(0, 0, window.position.size.x, window.position.size.y));
                GUILayout.BeginVertical();
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
            }

            var wrap = new GUIStyle()
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleCenter
            };

            GUILayout.Space(20f);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            textureMap = (Texture2D) EditorGUILayout.ObjectField("Normal Map", textureMap, typeof(Texture2D), false);

            if (GUILayout.Toggle(false, "Flip red channel"))
            {
                channel = Channels.Red;
            }

            if (GUILayout.Toggle(true, "Flip green channel (use this for normal maps)"))
            {
                channel = Channels.Green;
            }

            if (GUILayout.Toggle(false, "Flip blue channel"))
            {
                channel = Channels.Blue;
            }

            if (GUILayout.Toggle(false, "Flip alpha channel"))
            {
                channel = Channels.Alpha;
            }

            if (GUILayout.Button("Flip normal height"))
            {
                FlipChannel(channel);
            }

            saveToDifferentTexture =
                GUILayout.Toggle(false, "Check to save to a new texture. Leave unchecked to overwrite.");

            GUILayout.Space(10f);
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            if (!window) return;
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();

        }

        private void FlipChannel(Channels _channel)
        {
            var tempTexture = textureMap;
        }
    }
}
